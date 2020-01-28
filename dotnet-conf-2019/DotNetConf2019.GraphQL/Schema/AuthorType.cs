using FoundObjx.Blog.Data;
using HotChocolate.Types;

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
}
