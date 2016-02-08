using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SilverlightApplication1.AutoCompleteStates
{
    internal class AutoCompleteWatermark : AutoCompleteBase
    {
        public override event ErrorHandler OnError;
        public override event StateChangedHandler OnStateChanged;
        public override void Edit(string updatedText)
        {
            throw new NotImplementedException();
        }

        public override void EndEdit()
        {
            throw new NotImplementedException();
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override void ShowTooltip()
        {
            throw new NotImplementedException();
        }

        public AutoCompleteWatermark(AutoCompleteControl control)
            : base(control)
        {
            // If selection exists
            if (control.SelectedItem == null)
            {
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

            // TODO: Hide delete button            

            var txt = UserControl.ItemTextBox;
            txt.Foreground = new SolidColorBrush(Colors.LightGray);
            txt.Text = UserControl.Watermark;
            txt.KeyDown += txt_KeyDown;
        }

        void txt_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            UserControl.SetState(new AutoCompleteEditing(UserControl));
        }
    }
}
