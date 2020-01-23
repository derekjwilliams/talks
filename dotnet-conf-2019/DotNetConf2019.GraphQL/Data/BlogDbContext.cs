using DotNetConf2019.GraphQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DotNetConf2019.GraphQL.Data
{
    public class BlogDbContext : DbContext
    {
        private IConfiguration _config;
        public BlogDbContext(IConfiguration config)
        {
            _config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("IdentityConnection"));
        }

        public DbSet<Posts> Posts { get; set; }

        public DbSet<Comments> Comments { get; set; }

        public DbSet<Authors> Authors { get; set; }

        public DbSet<Images> Images { get; set; }
    }
}
