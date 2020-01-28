using FoundObjx.Blog.Data;
using HotChocolate.Types;
using HotChocolate;
using HotChocolate.Resolvers;
using Markdig;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoundObjx.Blog.Schema
{
    public class AuthorType : ObjectType<Author>
    {
        protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(a => a.PublicKey)
               .Type<NonNullType<UuidType>>();

            descriptor.Field(a => a.Name)
               .Type<NonNullType<StringType>>();
        }
    }
    public class CommentType : ObjectType<Comment>
    {
        protected override void Configure(IObjectTypeDescriptor<Comment> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(c => c.PublicKey)
                .Type<NonNullType<UuidType>>();

            descriptor.Field(c => c.PostId)
                .Ignore();

            descriptor.Field(c => c.Text)
                .Type<NonNullType<StringType>>();
        }
    }
    public class ImageSizeType : EnumType<ImageSize>
    {
    }
        public class ImageType : ObjectType<Image>
    {
        protected override void Configure(IObjectTypeDescriptor<Image> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(i => i.PublicKey)
               .Type<NonNullType<UuidType>>();

            descriptor.Field(i => i.PostId)
                .Ignore();

            descriptor.Field(p => p.Url)
                .Type<NonNullType<StringType>>();
        }
    }
    public class PostType : ObjectType<Post>
    {
        protected override void Configure(IObjectTypeDescriptor<Post> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(p => p.PublicKey)
                .Type<UuidType>(nullable: false);

            descriptor.Field(p => p.AuthorId)
                .Ignore();

            descriptor.Field(p => p.Title)
                .Type<StringType>(nullable: false);

            descriptor.Field("html")
                .Type<StringType>()
                .Resolver(ctx => Markdown.ToHtml(ctx.Parent<Post>().Markdown ?? ""));

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

        public async Task<Image> ResolveImage([Parent] Post post, [Service] BlogDbContext dbContext, ImageSize size)
        {
            return await dbContext.Images.FirstOrDefaultAsync(i => i.Size == size && i.PostId == post.PublicKey);
        }

        public async Task<Author> ResolveAuthor(IResolverContext context, [Parent] Post post, [Service] BlogDbContext dbContext)
        {
            var dataLoader = context.BatchDataLoader<Guid, Author>(nameof(ResolveAuthor), async authorIds =>
            {
                return await dbContext.Authors
                    .Where(a => authorIds.Contains(a.PublicKey))
                    .ToDictionaryAsync(a => a.PublicKey, a => a);
            });

            return await dataLoader.LoadAsync(post.AuthorId, context.RequestAborted);
        }

        public async Task<IReadOnlyList<Comment>> ResolveComments([Parent] Post post, [Service] BlogDbContext dbContext)
        {
            return await dbContext.Comments
                .Where(c => c.PostId == post.PublicKey)
                .OrderByDescending(c => c.SubmittedOn)
                .ToListAsync();
        }
    }
}
