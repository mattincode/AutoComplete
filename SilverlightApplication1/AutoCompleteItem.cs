namespace SilverlightApplication1
{
    public class AutoCompleteItem
    {
        public AutoCompleteItem(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public int Id{ get; set; }
        public string Text { get; set; }        
    }
}
