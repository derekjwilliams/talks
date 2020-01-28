﻿using FoundObjx.Blog.Data;
using FoundObjx.Blog.Schema;
using HotChocolate;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using NodaTime;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HotChocolate.AspNetCore.Voyager;
using System;

namespace FoundObjx.Blog
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IClock>(SystemClock.Instance);

            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<BlogDbContext>();

            services
                .AddDataLoaderRegistry()
                .AddGraphQL(sp =>
                     SchemaBuilder.New()
                        .AddServices(sp)
                        .AddQueryType<QueryType>()
                        .AddMutationType<MutationType>()
                        .AddType<OffsetDateTimeType>()
                        .AddType<Post>()
                        .Create());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                MigrateDatabase(app.ApplicationServices);
                app.UseDeveloperExceptionPage();
            }

            app
                .UseGraphQLHttpPost(new HttpPostMiddlewareOptions { Path = "/graphql" })
                .UseGraphQLHttpGetSchema(new HttpGetSchemaMiddlewareOptions { Path = "/graphql/schema" })
				.UsePlayground("/graphql")
				.UseVoyager("/graphql");
        }

        private void MigrateDatabase(IServiceProvider services)
        {
            var scopeFactory = services.GetRequiredService<IServiceScopeFactory>();

            using(var scope = scopeFactory.CreateScope())
            using (var context = scope.ServiceProvider.GetRequiredService<BlogDbContext>())
            {
                context.Database.Migrate();
            }
        }
    }
}
