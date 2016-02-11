using System.Windows;
using System.Windows.Media;
using SilverlightApplication1.Controls;

namespace SilverlightApplication1.AutoCompleteStates
{
    // Displays any databound items
    internal class AutoCompleteNormal : AutoCompleteBase
    {
        public override void Dispose()
        {
            UserControl.ClearBtn.Click -= ClearBtn_OnClick;
            UserControl.ItemTextBox.GotFocus -= ItemTextBox_GotFocus;
            UserControl.ItemTextBox.KeyDown -= ItemTextBox_KeyDown;          
        }

        public AutoCompleteNormal(AutoCompleteControl control)
            : base(control)
        {
            System.Diagnostics.Debug.WriteLine("AutoCompleteNormal");
            UserControl.ItemTextBox.GotFocus += ItemTextBox_GotFocus;
            UserControl.ItemTextBox.KeyDown += ItemTextBox_KeyDown;
            UserControl.ClearBtn.Click += ClearBtn_OnClick;
            UpdateUserInterface();
        }

        private void ItemTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Select all text
            UserControl.ItemTextBox.InnerTextBox.SelectAll();            
        }

        private void ItemTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            UserControl.SetState(new AutoCompleteEditing(UserControl));
        }

        private void ClearBtn_OnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            ClearEditedText();
        }

        private void ClearEditedText()
        {
            System.Diagnostics.Debug.WriteLine("Clear selection");
            UserControl.ItemTextBox.IsTextCompletionEnabled = false;
            UserControl.ItemTextBox.Text = "";
            UserControl.ItemTextBox.InnerTextBox.Text = "";
            UserControl.UserSetSelectedItem(null);            
            UserControl.SetState(new AutoCompleteWatermark(UserControl));
        }

        private void UpdateUserInterface()
        {
            UserControl.BorderBrush = new SolidColorBrush(Colors.Black);
            UserControl.ClearBtn.Visibility = Visibility.Visible;

            var txt = UserControl.ItemTextBox;
            txt.Foreground = new SolidColorBrush(Colors.Black);
            txt.FontStyle = FontStyles.Normal;
            txt.InnerTextBox.Text = UserControl.SelectedItem.Name;
            txt.Text = UserControl.SelectedItem.Name;
        }

    }

}
