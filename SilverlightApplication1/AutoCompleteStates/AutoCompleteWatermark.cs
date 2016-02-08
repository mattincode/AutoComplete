using System;
using System.Windows;
using System.Windows.Media;

namespace SilverlightApplication1.AutoCompleteStates
{
    internal class AutoCompleteWatermark : AutoCompleteBase
    {
        public override void Dispose()
        {
            var txt = UserControl.ItemTextBox;
            txt.KeyDown -= txt_KeyDown;
            txt.GotFocus -= txt_GotFocus;        
        }

        public AutoCompleteWatermark(AutoCompleteControl control)
            : base(control)
        {
            // If selection exists
            if (control.SelectedItem == null)
            {
                var txt = UserControl.ItemTextBox;
                txt.KeyDown += txt_KeyDown;
                txt.GotFocus += txt_GotFocus;
                UpdateUx();
            }
            else
            {
                control.SetState(new AutoCompleteNormal(control));
            }
        }

        private void UpdateUx()
        {            
            UserControl.ItemsListBox.Visibility = Visibility.Collapsed; // TODO: Each state should just draw the parts that belongs to it... not hiding other stuff!            
            UserControl.BorderBrush = new SolidColorBrush(Colors.Black);
            UserControl.ClearBtn.Visibility = Visibility.Collapsed;            
            // TODO: Hide delete button            

            var txt = UserControl.ItemTextBox;
            txt.Foreground = new SolidColorBrush(Colors.LightGray);
            txt.FontStyle = FontStyles.Italic;
            txt.Text = UserControl.Watermark;

        }

        void txt_GotFocus(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gotfocus");
            UserControl.SetState(new AutoCompleteEditing(UserControl));            
        }

        void txt_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("keydown");
            UserControl.SetState(new AutoCompleteEditing(UserControl));
        }
    }
}
