using System;
using System.Collections.Generic;
using System.Text;
using ExceptionManager.Interfaces;
using NUnit.Framework;
using Moq;

namespace ExceptionManager.Tests
{
    [TestFixture]
    public class ExceptionManagerTests
    {
        private IExceptionManager _exceptionManager;
        private Mock<IServerClient> _serverClient;
        private Mock<ExceptionManagerOptions> _options;
        [SetUp]
        public void Setup()
        {
            _serverClient = new Mock<IServerClient>();
            _options = new Mock<ExceptionManagerOptions>();
            _exceptionManager = new Implementations.ExceptionManager(_options.Object, _serverClient.Object);
        }

        [Test]
        public void IsCriticalException_When_CriticalException_Returns_True()
        {
            var exception = new NullReferenceException();
            _options.Setup(o => o.CriticalExceptionTypes)
                .Returns(new List<string>()
            {
                exception.GetType().ToString()
            });
            _exceptionManager = new Implementations.ExceptionManager(_options.Object, _serverClient.Object);

            var actual = _exceptionManager.IsCriticalException(exception);

            Assert.IsTrue(actual);
        }

        [Test]
        public void IsCriticalException_When_NonCriticalException_Returns_False()
        {
            var exception = new ApplicationException();

            var actual = _exceptionManager.IsCriticalException(exception);

            Assert.IsFalse(actual);
        }

        [Test]
        public void IsCriticalException_When_ExceptionNull_Returns_False()
        {
            Exception exception = null;
            _options.Setup(o => o.CriticalExceptionTypes)
                .Returns(new List<string>()
                {
                    typeof(NullReferenceException).ToString()
                });
            _exceptionManager = new Implementations.ExceptionManager(_options.Object, _serverClient.Object);

            

            var actual = _exceptionManager.IsCriticalException(exception);

            Assert.IsFalse(actual);
        }

        [Test]
        public void IsCriticalException_When_NoCriticalExceptionsInList_Returns_False()
        {
            var exception = new NullReferenceException();

            var actual = _exceptionManager.IsCriticalException(exception);

            Assert.IsFalse(actual);
        }
        
    }
}
