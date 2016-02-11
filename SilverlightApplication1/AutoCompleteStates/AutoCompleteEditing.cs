using System.Linq;
using System.Windows;
using System.Windows.Media;
using SilverlightApplication1.Controls;
using System.Windows.Input;

namespace SilverlightApplication1.AutoCompleteStates
{
    /// <summary>
    /// Represents the view when the user is editing the textbox.
    /// </summary>
    internal class AutoCompleteEditing : AutoCompleteBase
    {
        public AutoCompleteEditing(AutoCompleteControl control) : base(control)
        {
            System.Diagnostics.Debug.WriteLine("AutoCompleteEditing");
            UserControl.ClearBtn.Click += ClearBtn_OnClick;            
            UserControl.ItemTextBox.InnerTextBox.KeyDown += ItemTextBox_KeyDown; // We use the inner textbox since the autocomplete swallows the enter key
            UpdateUserInterface();
        }

        private void ItemTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Set the selected item on enter 
            if (e.Key == Key.Enter)
            {         
                if (UserControl.ItemTextBox.SelectedItem != null)
                {                    
                    System.Diagnostics.Debug.WriteLine("Selected Item set: {0}", UserControl.ItemTextBox.SelectedItem);
                    UserControl.SelectedItem = UserControl.Items.First(item => item.Name.Equals(UserControl.ItemTextBox.SelectedItem));
                }
            }
            else if (e.Key == Key.Escape)
            {
                if (UserControl.SelectedItem != null)
                {
                    // Reset the selecteditem
                    UserControl.ItemTextBox.IsTextCompletionEnabled = false;
                    UserControl.ItemTextBox.Text = UserControl.SelectedItem.Name;
                    UserControl.ItemTextBox.InnerTextBox.Text = UserControl.SelectedItem.Name;
                    UserControl.SetState(new AutoCompleteNormal(UserControl));
                }
                else
                {
                    ClearEditedText();
                }
                e.Handled = true;
            }
        }

        public override void Dispose()
        {            
            UserControl.ClearBtn.Click -= ClearBtn_OnClick;
            UserControl.ItemTextBox.InnerTextBox.KeyDown -= ItemTextBox_KeyDown;
        }


        private void ClearBtn_OnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            ClearEditedText();
        }

        private void ClearEditedText()
        {
            UserControl.ItemTextBox.IsTextCompletionEnabled = false;             
            UserControl.ItemTextBox.Text = "";
            UserControl.SelectedItem = null;       // Clear selected item    
            UserControl.SetState(new AutoCompleteWatermark(UserControl));
        }                

        private void UpdateUserInterface()
        {            
            var txt = UserControl.ItemTextBox;
            txt.InnerTextBox.Text = "";
            txt.Text = "";
            txt.FontStyle = FontStyles.Normal;
            txt.IsTextCompletionEnabled = true; // Populates the textbox with the topmost hit.
            txt.Foreground = new SolidColorBrush(Colors.Black);
            UserControl.ClearBtn.Visibility = Visibility.Visible;
            UserControl.ItemTextBox.ItemsSource = UserControl.Items.Select(item => item.Name);
        }
    }


}
