using ExceptionManager.Interfaces;
using System;

namespace ExceptionManager.Implementations
{
    public class ServerClient : IServerClient
    {
        public IWebClient Client { get; set; }
        public int ServerErrorCount { get; private set; } = 0;
        public bool SendExceptionData(Exception exception)
        {
            try
            {
                var message = $"Critical {exception.ToString()}: {exception.Message}" +
                              $"\nStack trace: {exception.StackTrace}\n";
                Client.SendData(message);
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
