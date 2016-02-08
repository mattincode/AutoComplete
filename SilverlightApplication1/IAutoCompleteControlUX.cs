using System.Windows.Controls;
using Telerik.Windows.Controls;
using ListBox = System.Windows.Controls.ListBox;

namespace SilverlightApplication1
{
    interface IAutoCompleteControlUx
    {
        TextBox ItemTextBox { get; }
        Border OuterBorder { get; }
        ListBox ItemsListBox { get; }
        RadButton ClearBtn { get; }
    }
}
