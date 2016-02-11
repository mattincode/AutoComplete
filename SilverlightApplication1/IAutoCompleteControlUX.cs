using System.Windows.Controls;
using SilverlightApplication1.Controls;
using Telerik.Windows.Controls;
using ListBox = System.Windows.Controls.ListBox;

namespace SilverlightApplication1
{
    interface IAutoCompleteControlUx
    {
        SelectableAutoCompleteBox ItemTextBox { get; }
        //Border OuterBorder { get; }
        RadButton ClearBtn { get; }
    }
}
