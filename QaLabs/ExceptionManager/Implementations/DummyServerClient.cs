using System;
using ExceptionManager.Interfaces;

namespace ExceptionManager.Implementations
{
    public class DummyServerClient : IServerClient
    {
        public int ServerErrorCount { get; private set; } = 0;
        public bool SendExceptionData(Exception exception)
        {
            Console.WriteLine($"Critical {exception.ToString()}: {exception.Message}\nAt {exception.StackTrace}");
            return true;
        }
    }
}
