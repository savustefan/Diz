using LucrareDisertatie.Data;
using LucrareDisertatie.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LucrareDisertatie.Repositories
{
    public class TagRepository : ITagRepository
    {
        private ApplicationDbContext _applicationDbContext;

        public TagRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Tag> AddTagAsync(Tag tag)
        {
            await _applicationDbContext.Tags.AddAsync(tag);
            await _applicationDbContext.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag?> DeleteTagAsync(Guid id)
        {
            var existingTag = await _applicationDbContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                _applicationDbContext.Tags.Remove(existingTag);
                await _applicationDbContext.SaveChangesAsync();
                return existingTag;
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            return await _applicationDbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetTagAsync(Guid id)
        {
            return _applicationDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateTagAsync(Tag tag)
        {
            var existTag = await _applicationDbContext.Tags.FindAsync(tag.Id);

            if (existTag != null)
            {
                existTag.Name = tag.Name;
                existTag.DisplayedName = tag.DisplayedName;

                await _applicationDbContext.SaveChangesAsync();

                return existTag;
            }

            return null;
        }
    }
}
