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
            // Subscribe to events
            UserControl.ClearBtn.Click += ClearBtn_OnClick;                        
            UserControl.ItemTextBox.InnerTextBox.KeyDown += ItemTextBox_KeyDown; // We use the inner textbox since the autocomplete swallows the enter key
            UserControl.ItemTextBox.DropDownClosing += ItemTextBox_DropDownClosing;
            UserControl.LostFocus += UserControl_LostFocus;
            // Update the controls
            UpdateUserInterface();
        }

        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {            
            if (UserControl.ItemTextBox.IsDropDownOpen) return;

            // Set correct state on lost focus                
            if (UserControl.SelectedItem != null)
            {
                UserControl.SetState(new AutoCompleteNormal(UserControl));
            }
            else
            {
                UserControl.SetState(new AutoCompleteWatermark(UserControl));
            }
        }

        private void ItemTextBox_DropDownClosing(object sender, System.Windows.Controls.RoutedPropertyChangingEventArgs<bool> e)
        {
            System.Diagnostics.Debug.WriteLine("DropDownClosing {0}", UserControl.ItemTextBox.SelectedItem);            
            if (UserControl.ItemTextBox.SelectedItem != null) 
            {
                TrySetSelectedItem(UserControl.ItemTextBox.SelectedItem.ToString());
            }            
        }

        public override void Dispose()
        {
            UserControl.ClearBtn.Click -= ClearBtn_OnClick;
            UserControl.ItemTextBox.InnerTextBox.KeyDown -= ItemTextBox_KeyDown;
            UserControl.ItemTextBox.DropDownClosing -= ItemTextBox_DropDownClosing;
            UserControl.LostFocus -= UserControl_LostFocus;         
        }
        
        private void ItemTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Set the selected item on enter 
            if (e.Key == Key.Enter)
            {         
                if (UserControl.ItemTextBox.SelectedItem != null)
                {                                        
                    TrySetSelectedItem(UserControl.ItemTextBox.SelectedItem.ToString());
                }
            }
            else if (e.Key == Key.Escape)
            {
                UserControl.ItemTextBox.IsDropDownOpen = false;
                if (UserControl.SelectedItem != null)
                {
                    UndoAndResetToSelectedItem();
                }
                else
                {
                    ClearEditedText();
                }
                e.Handled = true;
            }
        }

        private void ClearBtn_OnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            ClearEditedText();
        }        

        private bool TrySetSelectedItem(string selectedName)
        {
            if (UserControl.Items == null || selectedName == string.Empty) return false;
            var selectedItem = UserControl.Items.FirstOrDefault(item => item.Name.Equals(selectedName));
            if (selectedName != null)
            {
                UserControl.UserSetSelectedItem(selectedItem);                                
                return true;
            }
            return false;
        }

        private void UndoAndResetToSelectedItem()
        {
            // Reset the selecteditem
            UserControl.ItemTextBox.IsTextCompletionEnabled = false;
            UserControl.ItemTextBox.Text = UserControl.SelectedItem.Name;
            UserControl.ItemTextBox.InnerTextBox.Text = UserControl.SelectedItem.Name;
            UserControl.SetState(new AutoCompleteNormal(UserControl));
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
