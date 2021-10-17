using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Infrastructure.Cosmos.Models;
using WebApplication.Models;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MySettings>(Configuration.GetSection("WebApi"));

            // Sql
            services.AddTransient<Infrastructure.Sql.IProductRepository, Infrastructure.Sql.ProductRepository>();
            services.AddDbContext<Infrastructure.Sql.Models.ShopContext>(options => options.UseSqlServer(Configuration["WebApi:SqlConnection"]));

            // Cosmos
//            services.AddSingleton(new CosmosClientBuilder(Configuration["WebApi:CosmosConnection"])
            services.AddSingleton(new CosmosClientBuilder("AccountEndpoint=https://gunners-style.documents.azure.com:443/;AccountKey=IXtgNpP3aCIAxxRHyY85xjzPH4nC4ZcTIQZiOiQXqpHalEvEaqRCqmRaU2TbAuXrPzAT4JkbYxoOWSLhBafVDA==;")
                .WithConnectionModeDirect()
//                .WithCustomSerializer(new MyCosmosJsonSerializer())
                .Build());
            //            services.AddTransient<Infrastructure.Cosmos.IProductRepository, Infrastructure.Cosmos.ProductRepository>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<ShopProfile>();
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication", Version = "v1" });

                c.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                    {
                        return new[] { api.GroupName };
                    }

                    var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                    if (controllerActionDescriptor != null)
                    {
                        return new[] { controllerActionDescriptor.ControllerName };
                    }

                    throw new InvalidOperationException("Unable to determine tag for endpoint.");
                });
                c.DocInclusionPredicate((name, api) => true);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
