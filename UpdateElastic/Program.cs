using System;
using System.Threading;
using MassTransit;

namespace product
{
    class Program
    {
        private static readonly AutoResetEvent _closing = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(config =>
                        {
                            config.Host(new Uri($"rabbitmq://localhost"), host =>
                            {
                                host.Username("guest");
                                host.Password("guest");
                            });

                            config.ReceiveEndpoint("product.elastic.update", e =>
                            {
                                e.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(1)));
                                //e.Consumer<ImageViewProcessor>();
                            });
                        });

            bus.Start();

            _closing.WaitOne();
        }
    }
}