namespace LucrareDisertatie.Models.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayedName { get; set; }
        public ICollection<ContentPost> ContentPosts { get; set; }
    }
}
