using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FirstFloor.XamlSpy;
using SilverlightApplication1.AutoCompleteStates;
using Telerik.Windows.Controls;
using ListBox = System.Windows.Controls.ListBox;

namespace SilverlightApplication1
{
    public class AutoCompleteControl : UserControl, IAutoCompleteControl, IAutoCompleteControlUx
    {
        #region Properties

        public TextBox ItemTextBox { get; private set; }
        public Border OuterBorder { get; private set; }
        public ListBox ItemsListBox { get; private set; }
        public RadButton ClearBtn { get; private set; }

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
            var grid = new Grid() {HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch};
            // Clear button
            var btnImage = new Image() { Width = 16, Height = 16, Source = (ImageSource)new ImageSourceConverter().ConvertFromString("/SilverlightApplication1;component/Images/Remove.png"), HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center };
            ClearBtn = new RadButton() {Content = btnImage, Style = Application.Current.Resources["ClearButtonStyle"] as Style};
            // Item textbox
            ItemTextBox = new TextBox() { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, VerticalContentAlignment = VerticalAlignment.Stretch };
            grid.Children.Add(ItemTextBox);
            grid.Children.Add(ClearBtn);
            // Border
            OuterBorder = new Border() {Width = this.Width, Height = this.Height};
            OuterBorder.Child = grid;
            // Items list
            ItemsListBox = new ListBox();
            //TODO - Add listbox... where...
            Content = OuterBorder;
        }

    }



}
