using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Infrastructure.Cosmos.Models;

namespace WebApplication.Infrastructure.Cosmos
{
    public interface IProductRepository
    {
        Task<IList<Product>> GetAsync();

        Task<Product> GetByIdAsync(string id);

        Task InsertAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(Product product);
    }
}
