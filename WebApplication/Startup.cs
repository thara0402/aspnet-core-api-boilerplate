using AutoMapper;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
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
            services.AddDbContext<Infrastructure.Sql.Models.ShopContext>(options => options.UseSqlServer(Configuration["WebApi:SqlConnection"]));

            // Cosmos
            services.AddSingleton(new CosmosClientBuilder(Configuration["WebApi:CosmosConnection"])
                .WithConnectionModeDirect()
                .WithCustomSerializer(new MyCosmosJsonSerializer())
                .Build());

            // Table Storage
            services.AddSingleton(new TableServiceClient(Configuration["WebApi:StorageConnection"]));

            // Blob Storage
            var options = new BlobClientOptions
            (
                // workaround
                // https://github.com/Azure/azure-sdk-for-net/issues/14257
                BlobClientOptions.ServiceVersion.V2020_06_12
            );
            services.AddSingleton(new BlobServiceClient(Configuration["WebApi:StorageConnection"], options));

            // Queue Storage
            services.AddSingleton(new QueueClient(Configuration["WebApi:StorageConnection"], "sample-queue"));

            // Repository
            services.AddTransient<Infrastructure.Sql.IProductRepository, Infrastructure.Sql.ProductRepository>();
            services.AddTransient<Infrastructure.Cosmos.IProductRepository, Infrastructure.Cosmos.ProductRepository>();
            services.AddTransient<Infrastructure.Table.IProductRepository, Infrastructure.Table.ProductRepository>();
            services.AddTransient<Infrastructure.IFileRepository, Infrastructure.FileRepository>();
            services.AddTransient<Infrastructure.IMessageRepository, Infrastructure.MessageRepository>();

            // AutoMapper
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
