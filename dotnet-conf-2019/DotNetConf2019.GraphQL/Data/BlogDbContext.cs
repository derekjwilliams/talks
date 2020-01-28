using Bogus;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using System;

namespace FoundObjx.Blog.Data
{
    public class BlogDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;port=5432;userid=postgres;pwd=postgres;database=blog", npgOptionsBuilder =>
            {
                npgOptionsBuilder.UseNodaTime();
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder
                .ForNpgsqlUseIdentityColumns();

            Randomizer.Seed = new Random(13);
            var authors = new Faker<Author>()
                .RuleFor(a => a.PublicKey, f => Guid.NewGuid())
                .RuleFor(a => a.Name, f => f.Name.FullName())
                .Generate(5);

            var posts = new Faker<Post>()
                .RuleFor(p => p.PublicKey, f => Guid.NewGuid())
                .RuleFor(p => p.AuthorId, f => f.PickRandom(authors).PublicKey)
                .RuleFor(p => p.Title, f => f.Lorem.Sentence(5))
                .RuleFor(p => p.Markdown, f => f.Lorem.Paragraphs(3))
                .RuleFor(p => p.PublishedOn, f => OffsetDateTime.FromDateTimeOffset(f.Date.PastOffset()))
                .Generate(25);

            var comments = new Faker<Comment>()
                .RuleFor(c => c.PublicKey, f => Guid.NewGuid())
                .RuleFor(c => c.PostId, f => f.PickRandom(posts).PublicKey)
                .RuleFor(c => c.SubmittedOn, f => OffsetDateTime.FromDateTimeOffset(f.Date.PastOffset()))
                .RuleFor(c => c.Text, f => f.Lorem.Paragraphs(3))
                .Generate(100);

            var images = new Faker<Image>()
                .RuleFor(i => i.PublicKey, f => Guid.NewGuid())
                .RuleFor(i => i.PostId, f => f.PickRandom(posts).PublicKey)
                .RuleFor(i => i.Url, f => f.Image.PicsumUrl())
                .RuleFor(i => i.Size, f => f.PickRandom<ImageSize>())
                .Generate(100);

            modelBuilder.Entity<Author>().HasData(authors);
            modelBuilder.Entity<Post>().HasData(posts);
            modelBuilder.Entity<Comment>().HasData(comments);
            modelBuilder.Entity<Image>().HasData(images);
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Image> Images { get; set; }
    }
}
