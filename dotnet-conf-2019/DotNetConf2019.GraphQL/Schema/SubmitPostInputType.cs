using HotChocolate.Types;

namespace FoundObjx.Blog.Schema
{
    public class SubmitPostInputType : InputObjectType<SubmitPostInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<SubmitPostInput> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(i => i.AuthorId)
                .Type<NonNullType<UuidType>>();

            descriptor.Field(i => i.Title)
                .Type<NonNullType<StringType>>();

        }
    }
}
