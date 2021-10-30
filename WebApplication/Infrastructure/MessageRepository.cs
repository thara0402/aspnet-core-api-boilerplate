using Azure.Storage.Queues;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication.Infrastructure
{
    public class MessageRepository : IMessageRepository
    {
        private readonly QueueClient _queueClient;

        public MessageRepository(QueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        public async Task<IList<string>> ReceiveAsync()
        {
            await _queueClient.CreateIfNotExistsAsync();

            var result = new List<string>();
            var received = await _queueClient.ReceiveMessagesAsync(10, TimeSpan.FromSeconds(1));
            foreach (var message in received.Value)
            {
                result.Add(message.Body.ToString());

                await _queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt);
            }
            return result;
        }

        public async Task SendAsync(string message)
        {
            await _queueClient.CreateIfNotExistsAsync();

            await _queueClient.SendMessageAsync(message);
        }
    }
}
