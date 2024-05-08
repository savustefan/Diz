using LucrareDisertatie.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LucrareDisertatie.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LoginDbContext _loginDb;

        public UserRepository(LoginDbContext loginDb)
        {
            _loginDb = loginDb;
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await _loginDb.Users.ToListAsync();

            var adminUser = await _loginDb.Users
                .FirstOrDefaultAsync(x => x.Email == "admin@admin.com");

            if (adminUser != null)
            {
                users.Remove(adminUser);
            }

            return users;
        }
    }
}
