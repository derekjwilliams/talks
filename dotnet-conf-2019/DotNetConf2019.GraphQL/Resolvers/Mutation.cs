using DotNetConf2019.GraphQL.Data;
using DotNetConf2019.GraphQL.Schema;
using HotChocolate;
using System;
using System.Threading.Tasks;

namespace DotNetConf2019.GraphQL.Resolvers
{
    public class Mutation
    {
        //private readonly IClock clock;

        //public Mutation(IClock clock)
        //{
        //    this.clock = clock;
        //}

        public async Task<Posts> SubmitPost([Service] BlogDbContext dbContext, SubmitPostInput input)
        {
            var post = new Posts
            {
                AuthorId = input.AuthorId,
                Title = input.Title,
                Markdown = input.Markdown,
                PublishedOn = DateTime.Now
            };

            dbContext.Posts.Add(post);

            await dbContext.SaveChangesAsync();


            return post;
        }
    }
}
