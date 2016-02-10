using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SilverlightApplication1.Controls;

namespace SilverlightApplication1.AutoCompleteStates
{
    /// <summary>
    /// Represents the view when the user is editing the textbox.
    /// </summary>
    internal class AutoCompleteEditing : AutoCompleteBase
    {
        public AutoCompleteEditing(AutoCompleteControl control) : base(control)
        {
            UserControl.ClearBtn.Click += ClearBtn_OnClick;            
            DrawUserInterface();
        }

        public override void Dispose()
        {            
            UserControl.ClearBtn.Click -= ClearBtn_OnClick;            
        }


        private void ClearBtn_OnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            ClearEditedText();
        }

        private void ClearEditedText()
        {
            UserControl.ItemTextBox.Text = "";
            UserControl.ItemTextBox.Focus();
            UserControl.SetState(new AutoCompleteWatermark(UserControl));
        }                

        private void DrawUserInterface()
        {            
            var txt = UserControl.ItemTextBox;
            txt.Text = "";
            txt.FontStyle = FontStyles.Normal;
            txt.Foreground = new SolidColorBrush(Colors.Black);
            UserControl.ClearBtn.Visibility = Visibility.Visible;
            UserControl.ItemTextBox.ItemsSource = UserControl.Items.Select(item => item.Name);
        }
    }


}
