using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LucrareDisertatie.Data
{
    public class LoginDbContext : IdentityDbContext
    {
        public LoginDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //seed roles

            var userRoleId = "165a340f-87ff-430a-af6b-13c6a7551535";
            var creatorRoleId = "ba9bc350-5648-4f93-a258-e82565641bb1";
            var adminRoleId = "c7d5a3e0-8100-41c1-ac1e-228aa7308205";
            

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "creator",
                    NormalizedName = "creator",
                    Id = creatorRoleId,
                    ConcurrencyStamp = creatorRoleId
                },
                new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "user",
                    NormalizedName = "user",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }


            };

            builder.Entity<IdentityRole>().HasData(roles);

            // seed admin

            var adminId = "156239b5-c8a8-4917-bb01-4c9aac58df7e";

            var adminUser = new IdentityUser
            {
                UserName = "admin",
                Email = "admin@admin.com",
                NormalizedEmail = "admin@admin.com".ToUpper(),
                NormalizedUserName = "admin".ToUpper(),
                Id = adminId
            };

            adminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(adminUser, "admin");


            builder.Entity<IdentityUser>().HasData(adminUser);


            //add roles to admin

            var adminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = creatorRoleId,
                    UserId = adminId
                },

                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = adminId
                },

                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
