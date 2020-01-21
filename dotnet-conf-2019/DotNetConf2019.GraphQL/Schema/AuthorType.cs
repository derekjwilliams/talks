using DotNetConf2019.GraphQL.Data;
using HotChocolate.Types;

namespace DotNetConf2019.GraphQL.Schema
{
    public class AuthorType : ObjectType<Authors>
    {
        protected override void Configure(IObjectTypeDescriptor<Authors> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(a => a.Id)
               .Type<NonNullType<IdType>>();

            descriptor.Field(a => a.Name)
               .Type<NonNullType<StringType>>();
        }
    }
}
