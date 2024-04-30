namespace LucrareDisertatie.Models.Domain
{
    public class ContentPostComment
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public Guid ContentPostId { get; set; }

        public Guid UserId { get; set; }

        public DateTime TimeAdded { get; set; }
    }
}
