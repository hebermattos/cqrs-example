using Elasticsearch.Net;
using Microsoft.EntityFrameworkCore;
using Nest;
using products;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var settings = new ConnectionSettings(new Uri(builder.Configuration["ElasticSettings:baseUrl"]));

var defaultIndex = builder.Configuration["ElasticSettings:defaultIndex"];

settings.DefaultIndex(defaultIndex);

var client = new ElasticClient(settings);

builder.Services.AddSingleton<IElasticClient>(client);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
