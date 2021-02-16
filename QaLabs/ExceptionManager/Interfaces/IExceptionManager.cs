using System;

namespace ExceptionManager.Interfaces
{
    public interface IExceptionManager
    {
        ExceptionManagerOptions Options { set; }
        int CriticalExceptionCount { get; }
        int ExceptionCount { get; }
        bool IsCriticalException(Exception exception);
        void HandleException(Exception exception);
    }
}
