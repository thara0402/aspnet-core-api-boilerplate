using Azure;
using Azure.Data.Tables;
using System;

namespace WebApplication.Infrastructure.Table.Models
{
    public class Product : ITableEntity
    {
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public string Desc { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }
    }
}
