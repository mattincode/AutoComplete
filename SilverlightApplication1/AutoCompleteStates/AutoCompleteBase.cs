using System;
using System.Windows.Controls;
using SilverlightApplication1.Controls;

namespace SilverlightApplication1.AutoCompleteStates
{

    public abstract class AutoCompleteBase : IDisposable
    {
        protected AutoCompleteControl UserControl;        
        public delegate void ErrorHandler(string errorText);
        public event ErrorHandler OnError;

        protected void SetError(string errorText)
        {
            if (OnError != null)
            {
                OnError.Invoke(errorText);
            }
        }

        protected AutoCompleteBase(AutoCompleteControl control)
        {
            UserControl = control;            
        }

        public abstract void Dispose();
    }
}
