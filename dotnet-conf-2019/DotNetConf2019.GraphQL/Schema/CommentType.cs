using FoundObjx.Blog.Data;
using HotChocolate.Types;

namespace FoundObjx.Blog.Schema
{
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
}
