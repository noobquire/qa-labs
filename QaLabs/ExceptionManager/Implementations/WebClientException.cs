using System;

namespace ExceptionManager.Implementations
{
    [Serializable]
#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    public class WebClientException : ApplicationException
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
    {
        public WebClientException(string message) : base(message) { }

        public WebClientException() { }
    }
}
