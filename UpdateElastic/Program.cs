using MassTransit;
using Nest;
using products;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;

        var settings = new ConnectionSettings(new Uri(configuration["ElasticSettings:baseUrl"]));

        var defaultIndex = configuration["ElasticSettings:defaultIndex"];

        if (!string.IsNullOrEmpty(defaultIndex))
            settings = settings.DefaultIndex(defaultIndex);

        var client = new ElasticClient(settings);

        services.AddSingleton<IElasticClient>(client);

        services.AddMassTransit(x =>
        {
            x.AddConsumer<ElasticUpdater>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ConfigureEndpoints(context);
            });
        });
    })
    .Build();

await host.RunAsync();
