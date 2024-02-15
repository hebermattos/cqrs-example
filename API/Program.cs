using MassTransit;
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

settings = settings.DefaultIndex(defaultIndex);

var client = new ElasticClient(settings);

builder.Services.AddSingleton<IElasticClient>(client);

builder.Services.AddMassTransit(x =>
           {
               x.UsingRabbitMq((context, cfg) =>
               {
                   cfg.Host(builder.Configuration["RabbitMq:host"], "/", h =>
                {
                    h.Username(builder.Configuration["RabbitMq:user"]);
                    h.Password(builder.Configuration["RabbitMq:password"]);
                });

                   cfg.ConfigureEndpoints(context);
               });
           });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
