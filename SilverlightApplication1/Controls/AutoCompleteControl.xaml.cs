using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using SilverlightApplication1.AutoCompleteStates;

namespace SilverlightApplication1.Controls
{
    /// <summary>
    /// Autocomplete control with simple logic that only sets SelectedItem when the user selects it
    /// </summary>
    public partial class AutoCompleteControl : UserControl, IAutoCompleteControl
    {
        protected AutoCompleteBase CurrentState { get; set; }

        #region Dependency properties           
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<AutoCompleteItem>), typeof(UserControl), null);
        public ObservableCollection<AutoCompleteItem> Items
        {
            get { return (ObservableCollection<AutoCompleteItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark", typeof(string), typeof(UserControl), null);
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(AutoCompleteItem), typeof(UserControl), new PropertyMetadata(null, OnSelectedItemChanged));

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                var control = d as AutoCompleteControl;
                if (control == null) return;
                if (e.NewValue == null)
                {
                    control.SetState(new AutoCompleteWatermark(control));
                }
                else
                {
                    control.SetState(new AutoCompleteNormal(control));
                }
            }
        }

        public AutoCompleteItem SelectedItem
        {
            get { return (AutoCompleteItem)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        #endregion Dependency properties

        // Sets the selected item (from user initiated action)
        // This may be overriden to handle different types of selecteditems        
        internal void UserSetSelectedItem(AutoCompleteItem selectedItem)
        {
            SelectedItem = selectedItem;
            System.Diagnostics.Debug.WriteLine("Selected Item set: {0}", selectedItem.Name);
        }

        public void SetState(AutoCompleteBase newState)
        {
            if (CurrentState != null)
            {
                CurrentState.Dispose();
            }
            CurrentState = newState;
        }
        
        public AutoCompleteControl()
        {
            InitializeComponent();
            this.Loaded += (sender, args) =>
            {                
                // Set initial state
                CurrentState = new AutoCompleteWatermark(this);
            };
        }


    }
}
