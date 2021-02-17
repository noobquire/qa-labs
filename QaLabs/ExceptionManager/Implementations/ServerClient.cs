using ExceptionManager.Interfaces;
using System;

namespace ExceptionManager.Implementations
{
    public class ServerClient : IServerClient
    {
        private readonly IWebClient _client;

        public ServerClient(IWebClient client)
        {
            _client = client;
        }
        public int ServerErrorCount { get; private set; }
        public bool SendExceptionData(Exception exception)
        {
            try
            {
                var message = $"Critical {exception}: {exception.Message}" +
                              $"\nStack trace: {exception.StackTrace}\n";
                _client.SendData(message);
                Console.WriteLine(message);
            }
            catch (WebClientException)
            {
                Console.WriteLine($"An error occurred when sending data to the server");
                ServerErrorCount++;
                return false;
            }
            return true;
        }
    }
}
