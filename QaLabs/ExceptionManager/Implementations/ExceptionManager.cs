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

        public ExceptionManagerOptions Options
        {
            set
            {
                _criticalExceptionTypes = value?.CriticalExceptionTypes?.Select(Type.GetType).ToList();
            }
        }
        private List<Type> _criticalExceptionTypes;
        public List<Type> CriticalExceptionTypes { get; set; }
        public IServerClient _serverClient { get; set; }

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
