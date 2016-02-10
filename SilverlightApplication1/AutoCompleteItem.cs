namespace SilverlightApplication1
{
    public class AutoCompleteItem
    {
        public AutoCompleteItem(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id{ get; set; }
        public string Name { get; set; }        
    }
}
