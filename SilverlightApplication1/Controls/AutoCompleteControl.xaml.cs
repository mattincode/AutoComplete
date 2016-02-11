using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SilverlightApplication1.AutoCompleteStates;
using Telerik.Windows.Controls;

namespace SilverlightApplication1.Controls
{
    public partial class AutoCompleteControl : UserControl, IAutoCompleteControl, IAutoCompleteControlUx
    {
        protected AutoCompleteBase CurrentState { get; set; }
        public AutoCompleteBox ItemTextBox { get { return ItemTextBoxX; } }
        public RadButton ClearBtn { get { return ClearBtnX; } }

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

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(AutoCompleteItem), typeof(UserControl), null);
        public AutoCompleteItem SelectedItem
        {
            get { return (AutoCompleteItem)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        #endregion Dependency properties

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
