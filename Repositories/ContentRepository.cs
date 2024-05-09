using LucrareDisertatie.Data;
using LucrareDisertatie.Models.Domain;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ContentPost?> DeleteAsync(Guid id)
        {
            var presentContent = await _applicationDbContext.ContentPosts.FindAsync(id);

            if ( presentContent != null )
            {
                _applicationDbContext.ContentPosts.Remove(presentContent);
                await _applicationDbContext.SaveChangesAsync();

                return presentContent;
            }

            return null;
        }

        public async Task<IEnumerable<ContentPost>> GetAllAsync()
        {
            return await _applicationDbContext.ContentPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<ContentPost?> GetAsync(Guid id)
        {
            return await _applicationDbContext.ContentPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<ContentPost?> GetHandleUrlAsync(string handleUrl)
        {
            return await _applicationDbContext.ContentPosts
                                                .Include(x => x.Tags)
                                                .FirstOrDefaultAsync(x => x.HandleUrl == handleUrl);
        }


        public async Task<ContentPost?> UpdateAsync(ContentPost contentPost)
        {
            var existingContent = await _applicationDbContext.ContentPosts.Include(x => x.Tags).
                FirstOrDefaultAsync(x => x.ID == contentPost.ID);

            if (existingContent != null)
            {
                existingContent.ID = contentPost.ID;
                existingContent.Header = contentPost.Header;
                existingContent.Title = contentPost.Title;
                existingContent.Content = contentPost.Content;
                existingContent.Summary = contentPost.Summary;
                existingContent.Author = contentPost.Author;
                existingContent.ImageUrl = contentPost.ImageUrl;
                existingContent.HandleUrl = contentPost.HandleUrl;
                existingContent.PostDate = contentPost.PostDate;
                existingContent.Hidden = contentPost.Hidden;
                existingContent.Tags = contentPost.Tags;

                await _applicationDbContext.SaveChangesAsync();

                return existingContent;
            }

            return null;
        }
    }
}
