using Microsoft.AspNetCore.Identity;

namespace LucrareDisertatie.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();

    }
}
