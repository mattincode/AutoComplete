using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightApplication1.AutoCompleteStates
{
    /// <summary>
    /// Represents the view when the user is editing the textbox.
    /// </summary>
    internal class AutoCompleteEditing : AutoCompleteBase
    {
        public override event ErrorHandler OnError;
        public override event StateChangedHandler OnStateChanged;

        public override void Edit(string updatedText)
        {
            // Show listbox
            // Highlight any matching text in the listbox
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


        //TODO... We should add all the elements in the baseclass and don't remove them...
        public AutoCompleteEditing(AutoCompleteControl control)
            : base(control)
        {
            //var textBox = new TextBox();
            //textBox.BorderThickness = new Thickness(2);
            //textBox.Foreground = new SolidColorBrush(Colors.Black);
            ////textBox.Opacity = 0.6;
            //textBox.Width = Canvas.Width;
            //textBox.Height = Canvas.Height;
            //Canvas.Children.Add(textBox);
            //textBox.KeyDown += TextBox_KeyDown;
        }

        private void UpdateUx()
        {
            
        }
    }


}
