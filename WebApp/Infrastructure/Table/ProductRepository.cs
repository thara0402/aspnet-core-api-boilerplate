using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Infrastructure.Table.Models;

namespace WebApp.Infrastructure.Table
{
    public class ProductRepository : IProductRepository
    {
        private readonly TableClient _tableClient;
        private const string TableName = "Product";
        private const string PartitionKey = TableName;

        public ProductRepository(TableServiceClient tableServiceClient)
        {
            _tableClient = tableServiceClient.GetTableClient(TableName);
        }

        public async Task DeleteAsync(Product product)
        {
            await _tableClient.DeleteEntityAsync(product.PartitionKey, product.RowKey);
        }

        public async Task<IList<Product>> GetAsync()
        {
            var result = new List<Product>();
            var queryResults = _tableClient.QueryAsync<Product>(x => x.PartitionKey == PartitionKey);
            await foreach (var entity in queryResults)
            {
                result.Add(entity);
            }
            return result;
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _tableClient.GetEntityAsync<Product>(PartitionKey, id);
        }

        public async Task InsertAsync(Product product)
        {
            product.PartitionKey = PartitionKey;
            product.RowKey = Guid.NewGuid().ToString();
            await _tableClient.AddEntityAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            await _tableClient.UpdateEntityAsync(product, product.ETag);
        }
    }
}
