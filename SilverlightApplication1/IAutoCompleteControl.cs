using System.Windows.Controls;
using SilverlightApplication1.AutoCompleteStates;

namespace SilverlightApplication1
{
    interface IAutoCompleteControl
    {
        string Watermark { get; }
        //UserControl Control { get; }        
        AutoCompleteItem SelectedItem { get; }        
        void SetState(AutoCompleteBase newState);
    }
}
