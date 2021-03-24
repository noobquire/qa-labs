using System;

namespace ExceptionManager.Interfaces
{
    public interface IExceptionManager
    {
        int CriticalExceptionCount { get; }
        int ExceptionCount { get; }
        bool IsCriticalException(Exception exception);
        void HandleException(Exception exception);
    }
}
