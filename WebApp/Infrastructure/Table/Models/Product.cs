using Azure;
using Azure.Data.Tables;
using System;

namespace WebApp.Infrastructure.Table.Models
{
    public class Product : ITableEntity
    {
        public string PartitionKey { get; set; } = null!;

        public string RowKey { get; set; } = null!;

        public string Desc { get; set; } = null!;

        public string Name { get; set; } = null!;

        public double Price { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }
    }
}
