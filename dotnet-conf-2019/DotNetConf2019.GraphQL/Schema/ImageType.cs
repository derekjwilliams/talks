﻿using DotNetConf2019.GraphQL.Data;
using HotChocolate.Types;

namespace DotNetConf2019.GraphQL.Schema
{
    public class ImageType : ObjectType<Images>
    {
        protected override void Configure(IObjectTypeDescriptor<Images> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(i => i.Id)
               .Type<NonNullType<IdType>>();

            descriptor.Field(i => i.PostId)
                .Ignore();

            descriptor.Field(p => p.Url)
                .Type<NonNullType<StringType>>();
        }
    }
}
