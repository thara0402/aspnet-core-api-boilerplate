using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Infrastructure.Sql.Models;

namespace WebApplication.Infrastructure.Sql
{
    public interface IProductRepository
    {
        Task<IList<Product>> GetAsync();

        Task<Product> GetByIdAsync(long? id);

        Task InsertAsync(Product product);

        Task UpdateAsync();

        Task DeleteAsync(Product product);
    }
}
