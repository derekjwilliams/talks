using DotNetConf2019.GraphQL.Data;
using DotNetConf2019.GraphQL.Models;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetConf2019.GraphQL.Schema
{
    public class Query
    {
        public async Task<IReadOnlyList<Posts>> GetPosts([Service] BlogDbContext dbContext) =>
            await dbContext.Posts
                .OrderByDescending(p => p.PublishedOn)
                .ToListAsync();

        public Task<Posts> GetPost([Service] BlogDbContext dbContext, int id) =>
            dbContext.Posts.FindAsync(id).AsTask();

        public async Task<IReadOnlyList<Authors>> GetAuthors([Service] BlogDbContext dbContext) => 
            await dbContext.Authors.ToListAsync();

        public async Task<IReadOnlyList<Comments>> GetComments([Service] BlogDbContext dbContext) =>
            await dbContext.Comments.ToListAsync();

        public async Task<IReadOnlyList<Images>> GetImages([Service] BlogDbContext dbContext) =>
            await dbContext.Images.ToListAsync();


    }
}
