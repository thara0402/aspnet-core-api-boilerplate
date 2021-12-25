//using Microsoft.Azure.Cosmos;
//using Microsoft.Azure.Cosmos.Linq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using WebApp.Infrastructure.Cosmos.Models;

//namespace WebApp.Infrastructure.Cosmos
//{
//    public class ProductRepository : IProductRepository
//    {
//        private readonly Container _container;

//        public ProductRepository(CosmosClient cosmosClient)
//        {
//            _container = cosmosClient.GetContainer("Shop", "Product");
//        }

//        public async Task<IList<Product>> GetAsync()
//        {
//            var result = new List<Product>();

//            // PartitionKey を指定しないと Cross-Partition Query になるので注意
////          var queryRequestOptions = new QueryRequestOptions { PartitionKey = new PartitionKey("a9925e85-8156-4f11-8f88-ba5df5e673fa") };
//            var queryRequestOptions = new QueryRequestOptions { PartitionKey = null };

//            using (var iterator = _container.GetItemLinqQueryable<Product>(requestOptions: queryRequestOptions).ToFeedIterator())
//            {
//                do
//                {
//                    result.AddRange(await iterator.ReadNextAsync());

//                } while (iterator.HasMoreResults);
//            }
//            return result;
//        }

//        public async Task<Product> GetByIdAsync(string id)
//        {
//            var response = await _container.ReadItemAsync<Product>(id, new PartitionKey(id));
//            return response.Resource;
//        }

//        public async Task InsertAsync(Product product)
//        {
//            product.Id = Guid.NewGuid().ToString();
//            await _container.CreateItemAsync(product);
//        }

//        public async Task UpdateAsync(Product product)
//        {
//            await _container.ReplaceItemAsync(product, product.Id);
//        }

//        public async Task DeleteAsync(Product product)
//        {
//            await _container.DeleteItemAsync<Product>(product.Id, new PartitionKey(product.Id));
//        }
//    }
//}
