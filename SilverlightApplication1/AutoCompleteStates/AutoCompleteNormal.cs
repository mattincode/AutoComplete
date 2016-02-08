using System;

namespace SilverlightApplication1.AutoCompleteStates
{
    internal class AutoCompleteNormal : AutoCompleteBase
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

        public AutoCompleteNormal(AutoCompleteControl control)
            : base(control)
        {
        }
    }

}
