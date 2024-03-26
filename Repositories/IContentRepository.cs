using LucrareDisertatie.Models.Domain;

namespace LucrareDisertatie.Repositories
{
    public interface IContentRepository
    {
        Task<IEnumerable<ContentPost>> GetAllAsync();

        Task<ContentPost?> GetAsync(Guid id);

        Task<ContentPost> AddAsync(ContentPost contentPost);

        Task<ContentPost?> UpdateAsync(ContentPost contentPost);

        Task<ContentPost?> DeleteAsync(Guid id);
    }
}
