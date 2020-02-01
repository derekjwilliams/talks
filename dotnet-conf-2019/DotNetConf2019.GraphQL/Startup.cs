using FoundObjx.Blog.Data;
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
        readonly string origins = "origins"; // for Cross Request Scripting (CORS), see https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-3.1
        public void ConfigureServices(IServiceCollection services)
        {
          services.AddCors(options => {
            options.AddPolicy(origins,
              b => {
                  b.WithOrigins("http://localhost:3000") // change to approriate client url, or use "*" to leave wide open
                   .AllowAnyHeader() // required for POST
                   .AllowAnyMethod(); // required to allow for any header on preflight
              });
          });

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

            app.UseCors(origins) // user cors as configured in ConfigureService
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
