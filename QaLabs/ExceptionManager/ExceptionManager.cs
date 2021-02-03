using System;
using System.Collections.Generic;

namespace ExceptionManager
{
    public class ExceptionManager
    {
        public int CriticalExceptionCount { get; private set; }

        public int ExceptionCount { get; private set; }

        private readonly List<Type> _criticalExceptionTypes;

        public ExceptionManager()
        {
            _criticalExceptionTypes = new List<Type>()
            {
                typeof(IndexOutOfRangeException),
                typeof(NullReferenceException),
                typeof(InvalidCastException),
            };
        }

        public bool IsCriticalException(Exception exception)
        {
            return _criticalExceptionTypes.Contains(exception.GetType());
        }

        public void HandleException(Exception exception)
        {
            if(IsCriticalException(exception))
            {
                CriticalExceptionCount++;
            }
            else
            {
                ExceptionCount++;
            }
        }
    }
}
