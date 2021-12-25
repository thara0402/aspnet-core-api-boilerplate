using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Infrastructure.Sql.Models;

namespace WebApp.Infrastructure.Sql
{
    public interface IProductRepository
    {
        Task<IList<Product>> GetAsync();

        Task<Product?> GetByIdAsync(long? id);

        Task InsertAsync(Product product);

        Task UpdateAsync();

        Task DeleteAsync(Product product);
    }
}
