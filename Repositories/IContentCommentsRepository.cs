using LucrareDisertatie.Models.Domain;

namespace LucrareDisertatie.Repositories
{
    public interface IContentCommentsRepository
    {
        Task<ContentPostComment> AddAsync(ContentPostComment contentComment);

        Task<IEnumerable<ContentPostComment>> GetCommentsAsync(Guid contentPostId);
    }
}
