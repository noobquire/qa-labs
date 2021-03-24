using System;
using ExceptionManager.Interfaces;

namespace ExceptionManager.Implementations
{
    public class DummyWebClient : IWebClient
    {
        public void SendData(string data)
        {
            // Does nothing, actual implementation should send data to server
        }
    }
}
