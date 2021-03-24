using ExceptionManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExceptionManager.Implementations
{
    public class ExceptionManager : IExceptionManager
    {
        public int CriticalExceptionCount { get; private set; }

        public int ExceptionCount { get; private set; }
        
        private readonly List<Type> _criticalExceptionTypes;
        private readonly IServerClient _serverClient;

        public ExceptionManager(ExceptionManagerOptions options, IServerClient serverClient)
        {
            _criticalExceptionTypes = options?.CriticalExceptionTypes?.Select(Type.GetType).ToList();
            _serverClient = serverClient;
        }

        public bool IsCriticalException(Exception exception)
        {
            if (exception is null)
            {
                return false;
            }
            return _criticalExceptionTypes?.Contains(exception.GetType()) ?? false;
        }

        public void HandleException(Exception exception)
        {
            if (exception is null)
            {
                return;
            }
            if(IsCriticalException(exception))
            {
                _serverClient.SendExceptionData(exception);
                CriticalExceptionCount++;
            }
            else
            {
                ExceptionCount++;
            }
        }
    }
}
