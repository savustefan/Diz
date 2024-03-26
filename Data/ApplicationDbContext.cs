using LucrareDisertatie.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LucrareDisertatie.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ContentPost> ContentPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
