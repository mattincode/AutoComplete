using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using SilverlightApplication1.AutoCompleteStates;

namespace SilverlightApplication1
{
    public class AutoCompleteControl : UserControl, IAutoCompleteControl, IAutoCompleteControlUx
    {
        #region Properties

        public TextBox ItemTextBox { get; private set; }
        public Border OuterBorder { get; private set; }
        public ListBox ItemsListBox { get; private set; }

        #endregion Properties

        #region Dependency properties
        //public delegate void ItemSelectedHandler(int id);
        //public event ItemSelectedHandler OnItemSelected;             
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

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(AutoCompleteItem), typeof(UserControl), null);
        public AutoCompleteItem SelectedItem
        {
            get { return (AutoCompleteItem)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }        

        #endregion Dependency properties

        public UserControl Control
        {
            get { return this; }
        }

        public void SetState(AutoCompleteBase newState)
        {
            if (CurrentState != null)
            {
                CurrentState.Dispose();
            }
            CurrentState = newState;
        }

              
        protected AutoCompleteBase CurrentState { get; set; }
        public AutoCompleteControl()
        {                        
            this.Loaded += (sender, args) =>
            {
                IntitializeUx();
                // Set initial state
                CurrentState = new AutoCompleteWatermark(this);
            };                                            
        }
       
        // Draw the entire user interface 
        // The interaction behaviour is decided by the current state.
        public void IntitializeUx()
        {
            // Add 
            ItemTextBox = new TextBox();
            OuterBorder = new Border();
            OuterBorder.Child = ItemTextBox;
            ItemsListBox = new ListBox();            
            //textBox.BorderThickness = new Thickness(2);
            //textBox.Text = UserControl.Watermark;
            //textBox.Foreground = new SolidColorBrush(Colors.Gray);
            ////textBox.Opacity = 0.6;
            //textBox.Width = Canvas.Width;
            //textBox.Height = Canvas.Height;
            //Canvas.Children.Add(textBox);
            //textBox.KeyDown += TextBox_KeyDown;
        }

    }



}
