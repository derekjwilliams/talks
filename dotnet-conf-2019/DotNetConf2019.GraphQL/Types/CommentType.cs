using DotNetConf2019.GraphQL.Data;
using HotChocolate.Types;

namespace DotNetConf2019.GraphQL.Schema
{
    public class CommentType : ObjectType<Comments>
    {
        protected override void Configure(IObjectTypeDescriptor<Comments> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(c => c.Id)
                .Type<NonNullType<IdType>>();

            descriptor.Field(c => c.PostId)
                .Ignore();

            descriptor.Field(c => c.Text)
                .Type<NonNullType<StringType>>();

            //descriptor.Field(c => c.PublishedOn)
            //    .Type<NonNullType<DateTimeType>>();
        }
    }
}
