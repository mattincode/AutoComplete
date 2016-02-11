using System.Windows.Controls;

namespace SilverlightApplication1.Controls
{
    public class SelectableAutoCompleteBox : AutoCompleteBox
    {
        public SelectableAutoCompleteBox()
        {
            FilterMode = AutoCompleteFilterMode.None;
            MaxDropDownHeight = 350;
            MinimumPrefixLength = 2;
            Loaded += SelectableAutoCompleteBox_Loaded;
            TextChanged += SelectableAutoCompleteBox_TextChanged;      
        }

        private void SelectableAutoCompleteBox_TextChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            counter = 0;
        }

        int counter;
        int MaxItemsInDropDown = 50;
        private void SelectableAutoCompleteBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.TextFilter = (search, value) =>
            {
                System.Diagnostics.Debug.WriteLine(counter);
                if (value.Length > 0)
                {
                    if ((this.counter < MaxItemsInDropDown) &&
                        (value.ToUpper().StartsWith(search.ToUpper()) ||
                         value.ToUpper().Contains(search.ToUpper())))
                    {
                        this.counter++;
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            };
            
        }

        public TextBox InnerTextBox
        {
            get
            {
                return this.GetTemplateChild("Text") as TextBox;
            }
        }
    }
}
