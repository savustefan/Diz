using LucrareDisertatie.Models.Domain;

namespace LucrareDisertatie.Repositories
{
    public interface IRatingRepository
    {
        Task<int> GetTotalRatings(Guid contentPostId);

        Task <IEnumerable<Rating>> GetRatingsForContent(Guid contentPostId);

        Task <Rating> AddRatingContent(Rating rating);
    }
}
