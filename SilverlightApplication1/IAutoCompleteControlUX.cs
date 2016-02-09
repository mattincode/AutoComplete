using System.Windows.Controls;
using Telerik.Windows.Controls;
using ListBox = System.Windows.Controls.ListBox;

namespace SilverlightApplication1
{
    interface IAutoCompleteControlUx
    {
        AutoCompleteBox ItemTextBox { get; }
        //Border OuterBorder { get; }
        RadButton ClearBtn { get; }
    }
}
