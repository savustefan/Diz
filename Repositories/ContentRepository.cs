using LucrareDisertatie.Data;
using LucrareDisertatie.Models.Domain;

namespace LucrareDisertatie.Repositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ContentRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ContentPost> AddAsync(ContentPost contentPost)
        {
            await _applicationDbContext.AddAsync(contentPost);
            await _applicationDbContext.SaveChangesAsync();

            return contentPost;
        }

        public Task<ContentPost?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ContentPost>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ContentPost?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ContentPost?> UpdateAsync(ContentPost contentPost)
        {
            throw new NotImplementedException();
        }
    }
}
