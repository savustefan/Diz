using LucrareDisertatie.Data;
using LucrareDisertatie.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LucrareDisertatie.Repositories
{
    public class ContentPostCommentRepository : IContentCommentsRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ContentPostCommentRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<ContentPostComment> AddAsync(ContentPostComment contentComment)
        {
            await _applicationDbContext.ContentComments.AddAsync(contentComment);
            await _applicationDbContext.SaveChangesAsync();

            return contentComment;
        }

        public async Task<IEnumerable<ContentPostComment>> GetCommentsAsync(Guid contentPostId)
        {
            return await _applicationDbContext.ContentComments.Where(x => x.ContentPostId == contentPostId)
                .ToListAsync();
        }
    }
}
