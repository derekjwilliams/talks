using HotChocolate.Types;
using System;

namespace FoundObjx.Blog.Schema
{
    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(q => q.GetPosts(default))
                .Type<NonNullType<ListType<NonNullType<PostType>>>>();

            descriptor.Field(q => q.GetPost(default, default))
               .Argument("publicKey", a => a.Type<NonNullType<UuidType>>());
        }
    }
}
