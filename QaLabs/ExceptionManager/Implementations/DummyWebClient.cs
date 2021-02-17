using System;
using ExceptionManager.Interfaces;

namespace ExceptionManager.Implementations
{
    public class DummyWebClient : IWebClient
    {
        public void SendData(string data) { /*throw new NotImplementedException();*/ }
    }
}
