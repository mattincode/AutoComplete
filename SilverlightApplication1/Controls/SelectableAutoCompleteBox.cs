using System.Windows.Controls;

namespace SilverlightApplication1.Controls
{
    public class SelectableAutoCompleteBox : AutoCompleteBox
    {
        public TextBox InnerTextBox
        {
            get
            {
                return this.GetTemplateChild("Text") as TextBox;
            }
        }
    }
}
