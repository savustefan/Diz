namespace LucrareDisertatie.Models.Domain
{
    public class ContentPost
    {
        public Guid ID { get; set; }
        public string Header { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public string ImageUrl { get; set; }
        public string HandleUrl { get; set; }
        public DateTime PostDate { get; set; }
        public string Author { get; set; }
        public bool Hidden { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
