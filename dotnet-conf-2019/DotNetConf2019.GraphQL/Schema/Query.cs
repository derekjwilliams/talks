using FoundObjx.Blog.Data;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoundObjx.Blog.Schema
{
    public class Query
    {
        public async Task<IReadOnlyList<Post>> GetPosts([Service] BlogDbContext dbContext) =>
            await dbContext.Posts
                .OrderByDescending(p => p.PublishedOn)
                .ToListAsync();

        public async Task<IReadOnlyList<Author>> GetAuthors([Service] BlogDbContext dbContext) => 
            await dbContext.Authors.ToListAsync();

        public async Task<IReadOnlyList<Image>> GetImages([Service] BlogDbContext dbContext) =>
            await dbContext.Images.ToListAsync();

        public Task<Post> GetPost([Service] BlogDbContext dbContext, Guid PublicKey) {
            return dbContext.Posts.FindAsync(PublicKey);
        }
    }
}
