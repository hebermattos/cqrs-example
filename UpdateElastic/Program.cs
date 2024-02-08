using MassTransit;
using Nest;
using products;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;

        var settings = new ConnectionSettings(new Uri(configuration["ElasticSettings:baseUrl"]));

        var defaultIndex = configuration["ElasticSettings:defaultIndex"];

        settings = settings.DefaultIndex(defaultIndex);

        var client = new ElasticClient(settings);

        services.AddSingleton<IElasticClient>(client);

        services.AddMassTransit(x =>
        {
            x.AddConsumer<ElasticUpdater>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMq:host"], "/", h =>
                {
                    h.Username(configuration["RabbitMq:user"]);
                    h.Password(configuration["RabbitMq:password"]);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
    })
    .Build();

await host.RunAsync();
