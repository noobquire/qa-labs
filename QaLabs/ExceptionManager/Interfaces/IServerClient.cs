using System;

namespace ExceptionManager.Interfaces
{
    public interface IServerClient
    {
        int ServerErrorCount { get; }
        bool SendExceptionData(Exception exception);
    }
}
