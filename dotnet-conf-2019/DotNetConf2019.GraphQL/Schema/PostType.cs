using DotNetConf2019.GraphQL.Data;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Markdig;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetConf2019.GraphQL.Schema
{
    public class PostType : ObjectType<Posts>
    {
        protected override void Configure(IObjectTypeDescriptor<Posts> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(p => p.Id)
                .Type<IdType>(nullable: false);

            descriptor.Field(p => p.AuthorId)
                .Ignore();

            descriptor.Field(p => p.Title)
                .Type<StringType>(nullable: false);

            descriptor.Field("html")
                .Type<StringType>()
                .Resolver(ctx => Markdown.ToHtml(ctx.Parent<Posts>().Markdown ?? ""));

            descriptor.Field<PostType>(p => ResolveAuthor(default, default, default))
                .Name("author")
                .Type<AuthorType>(nullable: false);

            descriptor.Field<PostType>(p => ResolveComments(default, default))
                .Name("comments")
                .ListType<CommentType>();

            descriptor.Field<PostType>(p => ResolveImage(default, default, default))
                .Name("image")
                .Argument("size", a => a.Type<ImageSizeType>(nullable: false).DefaultValue(ImageSize.Small))
                .Type<ImageType>();
        }

        public async Task<Images> ResolveImage([Parent] Posts post, [Service] BlogDbContext dbContext, ImageSize size)
        {
            return await dbContext.Images.FirstOrDefaultAsync(i => i.Size == size && i.PostId == post.Id);
        }

        public async Task<Authors> ResolveAuthor(IResolverContext context, [Parent] Posts post, [Service] BlogDbContext dbContext)
        {
            var dataLoader = context.BatchDataLoader<int, Authors>(nameof(ResolveAuthor), async authorIds =>
            {
                return await dbContext.Authors
                    .Where(a => authorIds.Contains(a.Id))
                    .ToDictionaryAsync(a => a.Id, a => a);
            });

            return await dataLoader.LoadAsync(post.AuthorId, context.RequestAborted);
        }

        public async Task<IReadOnlyList<Comments>> ResolveComments([Parent] Posts post, [Service] BlogDbContext dbContext)
        {
            return await dbContext.Comments
                .Where(c => c.PostId == post.Id)
                .OrderByDescending(c => c.SubmittedOn)
                .ToListAsync();
        }
    }
}
