using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionManager.Interfaces
{
    public interface IServerClient
    {
        int ServerErrorCount { get; }
        bool SendExceptionData(Exception exception);
    }
}
