﻿using HotChocolate.Types;

namespace FoundObjx.Blog.Schema
{
    public class MutationType : ObjectType<Mutation>
    {
        protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field(m => m.SubmitPost(default, default))
                .Type<PostType>()
                .Argument("input", a => a.Type<SubmitPostInputType>(nullable: false));
        }
    }
}
