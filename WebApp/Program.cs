using AutoMapper;
using Azure.Data.Tables;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using WebApp;
using WebApp.Infrastructure.Cosmos.Models;
using WebApp.Infrastructure.Sql.Models;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

if (!builder.Environment.IsDevelopment())
{
    builder.Configuration.AddAzureKeyVault(new Uri(builder.Configuration["KeyVaultUrl"] ?? ""), new DefaultAzureCredential());
}

builder.Services.Configure<MySettings>(builder.Configuration.GetSection("WebApi"));

// Application Insights
builder.Services.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions { ConnectionString = builder.Configuration["WebApi:ConnectionString"] });

// Sql
builder.Services.AddDbContext<ShopContext>(options => options.UseSqlServer(builder.Configuration["WebApi:SqlConnection"]));

// Cosmos
builder.Services.AddSingleton(new CosmosClientBuilder(builder.Configuration["WebApi:CosmosConnection"])
    .WithConnectionModeDirect()
    .WithCustomSerializer(new MyCosmosJsonSerializer())
    .Build());

// Table Storage
builder.Services.AddSingleton(new TableServiceClient(builder.Configuration["WebApi:StorageConnection"]));

// Blob Storage
var options = new BlobClientOptions
(
    // workaround
    // https://github.com/Azure/azure-sdk-for-net/issues/14257
    BlobClientOptions.ServiceVersion.V2020_06_12
);
builder.Services.AddSingleton(new BlobServiceClient(builder.Configuration["WebApi:StorageConnection"], options));

// Queue Storage
builder.Services.AddSingleton(new QueueClient(builder.Configuration["WebApi:StorageConnection"], "sample-queue"));

// Repository
builder.Services.AddTransient<WebApp.Infrastructure.Sql.IProductRepository, WebApp.Infrastructure.Sql.ProductRepository>();
builder.Services.AddTransient<WebApp.Infrastructure.Cosmos.IProductRepository, WebApp.Infrastructure.Cosmos.ProductRepository>();
builder.Services.AddTransient<WebApp.Infrastructure.Table.IProductRepository, WebApp.Infrastructure.Table.ProductRepository>();
builder.Services.AddTransient<WebApp.Infrastructure.IFileRepository, WebApp.Infrastructure.FileRepository>();
builder.Services.AddTransient<WebApp.Infrastructure.IMessageRepository, WebApp.Infrastructure.MessageRepository>();

// AutoMapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile<ShopProfile>();
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// API Controller
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication", Version = "v1" });

    c.TagActionsBy(api =>
    {
        if (api.GroupName != null)
        {
            return [api.GroupName];
        }

        var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
        if (controllerActionDescriptor != null)
        {
            return [controllerActionDescriptor.ControllerName];
        }

        throw new InvalidOperationException("Unable to determine tag for endpoint.");
    });
    c.DocInclusionPredicate((name, api) => true);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseSwagger(c =>
{
    c.OpenApiVersion = OpenApiSpecVersion.OpenApi3_1;
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API V1");
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
