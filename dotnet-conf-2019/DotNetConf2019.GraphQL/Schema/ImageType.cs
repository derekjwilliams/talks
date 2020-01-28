using FoundObjx.Blog.Data;
using HotChocolate.Types;

namespace FoundObjx.Blog.Schema
{
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
}
