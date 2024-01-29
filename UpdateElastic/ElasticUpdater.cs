using MassTransit;

namespace products
{
    public class ElasticUpdater : IConsumer<Product>
    {
        public Task Consume(ConsumeContext<Product> context)
        {
            throw new NotImplementedException();
        }
    }
}