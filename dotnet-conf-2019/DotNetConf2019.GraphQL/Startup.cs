﻿using DotNetConf2019.GraphQL.Data;
using DotNetConf2019.GraphQL.Schema;
using HotChocolate;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HotChocolate.AspNetCore.Voyager;
using System;

namespace DotNetConf2019.GraphQL
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<BlogDbContext>()
                .AddDataLoaderRegistry()
                .AddGraphQL(sp =>
                     SchemaBuilder.New()
                        .AddServices(sp)
                        .AddQueryType<QueryType>()
                        .AddMutationType<MutationType>()
                        .AddType<OffsetDateTimeType>()
                        //.AddType<Posts>()  //Derek...  Posts query still works without this.  Do we still need it? 
                        .Create());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseGraphQLHttpPost(new HttpPostMiddlewareOptions { Path = "/graphql" })
                .UseGraphQLHttpGetSchema(new HttpGetSchemaMiddlewareOptions { Path = "/graphql/schema" })
				.UsePlayground("/graphql")
				.UseVoyager("/graphql");
        }

    }
}
