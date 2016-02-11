using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using SilverlightApplication1.AutoCompleteStates;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Buttons;
using ListBox = System.Windows.Controls.ListBox;

namespace SilverlightApplication1
{
    public class xxxAutoCompleteControl : UserControl, IAutoCompleteControl, IAutoCompleteControlUx
    {
        #region Properties

        public AutoCompleteBox ItemTextBox { get; private set; }
        public Border OuterBorder { get; private set; }
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
        public xxxAutoCompleteControl()
        {                        
            this.Loaded += (sender, args) =>
            {
                IntitializeUx();
                // Set initial state
                //CurrentState = new AutoCompleteWatermark(this);
            };                                            
        }
        string AutoCompleteItemTemplate = "<TypeAheadHighlightningControl";

        // 
        private DataTemplate GetAutoCompleteItemTemplate(string dataItemControlNamespace)
        {
            var sb = new StringBuilder();
            sb.Append("<DataTemplate ");
            sb.Append("xmlns='http://schemas.microsoft.com/winfx/");
            sb.Append("2006/xaml/presentation' ");
            sb.Append("xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' ");
            sb.Append("xmlns:controls='clr-namespace:" + dataItemControlNamespace + ";assembly=SilverlightApplication1'> ");
            sb.Append("<controls:TypeAheadHighlightningControl Name='{Binding Name}' HighlightedText='{ Binding Name, ElementName = AutoCompleteTxt}'/>");
            sb.Append("</DataTemplate>");                        
            return XamlReader.Load(sb.ToString()) as DataTemplate;
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
            ItemTextBox = new AutoCompleteBox() { Name="AutoCompleteTxt", HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, VerticalContentAlignment = VerticalAlignment.Stretch };
            var itemBinding = new Binding("ItemBinding");
            itemBinding.Source = Items;
            itemBinding.Mode = BindingMode.OneWay;
            itemBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
//            BindingOperations.SetBinding(ItemTextBox, AutoCompleteBox.ItemsSourceProperty, itemBinding);
            //ItemTextBox.ItemsSource = Items;
            ItemTextBox.ItemTemplate = GetAutoCompleteItemTemplate("SilverlightApplication1.Controls");
            ItemTextBox.ItemsSource = _list;
            grid.Children.Add(ItemTextBox);
            grid.Children.Add(ClearBtn);
            // Border
            OuterBorder = new Border() {Width = this.Width, Height = this.Height};
            OuterBorder.Child = grid;            
            Content = OuterBorder;
        }
        ObservableCollection<AutoCompleteItem> _list = new ObservableCollection<AutoCompleteItem>() { new AutoCompleteItem(1, "12345 Mathias Andersson"), new AutoCompleteItem(2, "12366 Olle Nilsson") };

    }



}
