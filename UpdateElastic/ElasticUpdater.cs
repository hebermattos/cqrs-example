using MassTransit;
using Nest;
using Newtonsoft.Json;

namespace products
{
    public class ElasticUpdater : IConsumer<Product>
    {
        private IElasticClient _elasticClient;

        public ElasticUpdater(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task Consume(ConsumeContext<Product> context)
        {
            Console.WriteLine("...........recebeu.............");

            var product = context.Message;

            Console.WriteLine(JsonConvert.SerializeObject(product));

            var data = await _elasticClient.UpdateAsync<Product>(product.Id, u => u
                            .Doc(product)
                            .DocAsUpsert());

            Console.WriteLine(JsonConvert.SerializeObject(data));
        }
    }
}