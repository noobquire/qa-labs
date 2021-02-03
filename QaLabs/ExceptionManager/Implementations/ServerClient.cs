using System;
using System.Net;
using System.Net.Http;
using System.Text;
using ExceptionManager.Interfaces;

namespace ExceptionManager.Implementations
{
    public class ServerClient : IServerClient
    {
        private readonly IWebClient _client;

        public ServerClient(IWebClient client)
        {
            _client = client;
        }
        public int ServerErrorCount { get; private set; } = 0;
        public bool SendExceptionData(Exception exception)
        {
            try
            {
                var message = $"Critical {exception.ToString()}: {exception.Message}" +
                              $"\nStack trace: {exception.StackTrace}\n";
                _client.SendData(message);
                Console.WriteLine(message);
            }
            catch (WebClientException)
            {
                Console.WriteLine($"An error occurred when sending data to the server");
                ServerErrorCount++;
            }
            return true;
        }
    }
}
