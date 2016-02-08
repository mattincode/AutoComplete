using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SilverlightApplication1.AutoCompleteStates
{
    /// <summary>
    /// Represents the view when the user is editing the textbox.
    /// </summary>
    internal class AutoCompleteEditing : AutoCompleteBase
    {        
        public override void Dispose()
        {
            var clearBtn = UserControl.ClearBtn;
            clearBtn.Click -= clearBtn_OnClick;
        }
        
        public AutoCompleteEditing(AutoCompleteControl control)
            : base(control)
        {
            var clearBtn = UserControl.ClearBtn;
            clearBtn.Click += clearBtn_OnClick;
            UpdateUx();
        }

        private void clearBtn_OnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            UserControl.ItemTextBox.Text ="";
            UserControl.ItemTextBox.Focus();
            UserControl.SetState(new AutoCompleteWatermark(UserControl));
        }                       

        private void UpdateUx()
        {
            System.Diagnostics.Debug.WriteLine("Update editing");
            var txt = UserControl.ItemTextBox;
            txt.Text = "";
            txt.FontStyle = FontStyles.Normal;
            txt.Foreground = new SolidColorBrush(Colors.Black);
            UserControl.ClearBtn.Visibility = Visibility.Visible;

        }
    }


}
