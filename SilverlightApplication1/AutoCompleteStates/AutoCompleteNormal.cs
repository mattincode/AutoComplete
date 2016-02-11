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
            UserControl.ItemTextBox.GotFocus -= ItemTextBox_GotFocus;
            UserControl.ItemTextBox.KeyDown -= ItemTextBox_KeyDown;
        }

        public AutoCompleteNormal(AutoCompleteControl control)
            : base(control)
        {
            System.Diagnostics.Debug.WriteLine("AutoCompleteNormal");
            UserControl.ItemTextBox.GotFocus += ItemTextBox_GotFocus;
            UserControl.ItemTextBox.KeyDown += ItemTextBox_KeyDown;
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

        private void UpdateUserInterface()
        {
            UserControl.BorderBrush = new SolidColorBrush(Colors.Black);
            UserControl.ClearBtn.Visibility = Visibility.Collapsed;

            var txt = UserControl.ItemTextBox;
            txt.Foreground = new SolidColorBrush(Colors.Black);
            txt.FontStyle = FontStyles.Normal;
            txt.Text = UserControl.SelectedItem.Name;
        }

    }

}
