using System;

namespace ExceptionManager.Implementations
{
    [Serializable]
    public class WebClientException : ApplicationException
    {
        public WebClientException(string message) : base(message) { }

        public WebClientException() { }
    }
}
