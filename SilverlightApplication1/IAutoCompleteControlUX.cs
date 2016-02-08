using System.Windows.Controls;

namespace SilverlightApplication1
{
    interface IAutoCompleteControlUx
    {
        TextBox ItemTextBox { get; }
        Border OuterBorder { get; }
        ListBox ItemsListBox { get; }    
    }
}
