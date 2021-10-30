using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication.Infrastructure
{
    public interface IMessageRepository
    {
        Task SendAsync(string message);

        Task<IList<string>> ReceiveAsync();
    }
}
