using MassTransit;
using Nest;
using products;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ProductsContext>();

var settings = new ConnectionSettings(new Uri(builder.Configuration["ElasticSettings:baseUrl"]));

var defaultIndex = builder.Configuration["ElasticSettings:defaultIndex"];

if (!string.IsNullOrEmpty(defaultIndex))
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
