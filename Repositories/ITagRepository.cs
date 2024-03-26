using LucrareDisertatie.Models.Domain;

namespace LucrareDisertatie.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable <Tag>>    GetAllTagsAsync();

        Task<Tag?> GetTagAsync(Guid id);

        Task<Tag> AddTagAsync(Tag tag);

        Task<Tag?> UpdateTagAsync(Tag tag);

        Task<Tag?> DeleteTagAsync(Guid id);

    }
}
