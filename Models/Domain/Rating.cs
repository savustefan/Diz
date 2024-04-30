namespace LucrareDisertatie.Models.Domain
{
    public class Rating
    {
        public Guid Id { get; set; }
        public Guid ContentPostId { get; set; }
        public Guid UserId { get; set; }
    }
}
