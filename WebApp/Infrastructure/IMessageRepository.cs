using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.Infrastructure
{
    public interface IMessageRepository
    {
        Task SendAsync(string message);

        Task<IList<string>> ReceiveAsync();
    }
}
