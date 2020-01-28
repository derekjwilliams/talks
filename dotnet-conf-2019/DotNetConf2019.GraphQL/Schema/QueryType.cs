using HotChocolate.Types;

namespace FoundObjx.Blog.Schema
{
    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(q => q.GetPost(default, default))
               .Argument("PublicKey", a => a.Type<NonNullType<UuidType>>());
        }
    }
}
