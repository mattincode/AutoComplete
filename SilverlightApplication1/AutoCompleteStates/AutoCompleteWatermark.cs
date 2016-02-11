using System;
using System.Windows;
using System.Windows.Media;
using SilverlightApplication1.Controls;

namespace SilverlightApplication1.AutoCompleteStates
{
    // Presents a watermark until editing begins
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
            System.Diagnostics.Debug.WriteLine("AutoCompleteWatermark");
            // If selection exists
            if (control.SelectedItem == null)
            {
                var txt = UserControl.ItemTextBox;
                txt.KeyDown += txt_KeyDown;
                txt.GotFocus += txt_GotFocus;
                UpdateUserInterface();
            }
            else
            {
                control.SetState(new AutoCompleteNormal(control));
            }
        }

        private void UpdateUserInterface()
        {                             
            UserControl.BorderBrush = new SolidColorBrush(Colors.Black);
            UserControl.ClearBtn.Visibility = Visibility.Collapsed;                 

            var txt = UserControl.ItemTextBox;
            txt.Foreground = new SolidColorBrush(Colors.DarkGray);
            txt.FontStyle = FontStyles.Italic;
            txt.Text = UserControl.Watermark;           
            System.Diagnostics.Debug.WriteLine("Watermark set to: {0}", UserControl.Watermark);
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
