using System;
using System.Windows.Controls;

namespace SilverlightApplication1.AutoCompleteStates
{

    public abstract class AutoCompleteBase : IDisposable
    {
        protected AutoCompleteControl UserControl;
        protected Canvas Canvas;        
        public delegate void StateChangedHandler(AutoCompleteBase newState);
        public delegate void ErrorHandler();

        public abstract event ErrorHandler OnError;
        public abstract event StateChangedHandler OnStateChanged; 

        protected AutoCompleteBase(AutoCompleteControl control)
        {
            UserControl = control;
            Canvas = new Canvas() {Width = control.Control.Width, Height = control.Control.Height};
            UserControl.Control.Content = Canvas;
        }
        public abstract void Edit(string updatedText);
        public abstract void EndEdit();
        public abstract void Delete();
        public abstract void ShowTooltip();

        public void Dispose()
        {
            if (Canvas != null)
            {
                Canvas.Children.Clear();                
            }            
        }

    }
