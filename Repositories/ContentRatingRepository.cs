
using LucrareDisertatie.Data;
using LucrareDisertatie.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LucrareDisertatie.Repositories
{
    public class ContentRatingRepository : IRatingRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ContentRatingRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Rating> AddRatingContent(Rating rating)
        {
            await _applicationDbContext.Rating.AddAsync(rating);
            await _applicationDbContext.SaveChangesAsync();
            return rating;
        }

        public async Task<IEnumerable<Rating>> GetRatingsForContent(Guid contentPostId)
        {
            return await _applicationDbContext.Rating.Where(x => x.ContentPostId == contentPostId).ToListAsync();
        }

        public async Task<int> GetTotalRatings(Guid contentPostId)
        {
            return await _applicationDbContext.Rating.CountAsync(x => x.ContentPostId == contentPostId);
        }
    }
}
