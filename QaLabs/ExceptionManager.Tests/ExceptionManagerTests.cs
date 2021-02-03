using System;
using System.Collections.Generic;
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
            SetupCriticalException(exception);

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
            SetupCriticalException(new NullReferenceException());

            var actual = _exceptionManager.IsCriticalException(null);

            Assert.IsFalse(actual);
        }

        [Test]
        public void IsCriticalException_When_NoCriticalExceptionsInList_Returns_False()
        {
            var exception = new NullReferenceException();

            var actual = _exceptionManager.IsCriticalException(exception);

            Assert.IsFalse(actual);
        }

        [Test]
        public void HandleException_When_CriticalException_Increments_CriticalExceptionCount()
        {
            var exception = new NullReferenceException();
            SetupCriticalException(exception);
            var expectedCount = 1;

            _exceptionManager.HandleException(exception);
            var actualCount = _exceptionManager.CriticalExceptionCount;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void HandleException_When_NonCriticalException_Increments_ExceptionCount()
        {
            var exception = new ApplicationException();
            var expectedCount = 1;

            _exceptionManager.HandleException(exception);
            var actualCount = _exceptionManager.ExceptionCount;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void HandleException_When_ExceptionNull_Counters_NotIncremented()
        {
            _exceptionManager.HandleException(null);
            var actualCount = _exceptionManager.ExceptionCount;
            var actualCriticalCount = _exceptionManager.CriticalExceptionCount;
            
            Assert.AreEqual(0, actualCriticalCount);
            Assert.AreEqual(0, actualCount);
        }

        private void SetupCriticalException(Exception exception)
        {
            _options.Setup(o => o.CriticalExceptionTypes)
                .Returns(new List<string>()
                {
                    exception.GetType().ToString()
                });
            _exceptionManager = new Implementations.ExceptionManager(_options.Object, _serverClient.Object);
        }
    }
}
