using System;
using ExceptionManager.Interfaces;

namespace ExceptionManager
{
    public class App
    {
        private readonly IExceptionManager _exceptionManager;

        public App(IExceptionManager exceptionManager)
        {
            _exceptionManager = exceptionManager;
        }

        public void Run()
        {
            var exceptions = new Exception[]
            {
                new IndexOutOfRangeException("Index was out of range"),
                new InvalidOperationException("This operation is not permitted"),
                new NullReferenceException("Data was null"),
                new InvalidCastException("Cannot cast TypeA to TypeB"),
                new AccessViolationException("Program is not permitted to do this action"),
                new ApplicationException("Business logic validation failure")
            };

            foreach (var exception in exceptions)
            {
                try
                {
                    throw exception;
                }
                catch (Exception e)
                {
                    _exceptionManager.HandleException(e);
                }
            }

            Console.WriteLine($"Exception count: {_exceptionManager.ExceptionCount}");
            Console.WriteLine($"Critical exception count: {_exceptionManager.CriticalExceptionCount}");

            Console.ReadKey();
        }
    }
}
