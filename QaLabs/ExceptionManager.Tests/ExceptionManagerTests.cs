using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using ExceptionManager.Implementations;
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
            _exceptionManager = new Implementations.ExceptionManager()
            {
                _serverClient = _serverClient.Object,
                Options = _options.Object
            };
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

        [TestCase(typeof(NullReferenceException), true)]
        [TestCase(typeof(InvalidCastException), true)]
        [TestCase(typeof(HttpRequestException), false)]
        [TestCase(typeof(ApplicationException), false)]
        [TestCase(typeof(WebClientException), false)]
        [TestCase(typeof(Exception), false)]
        public void IsCriticalException_When_Exception_Expected(Type exceptionType, bool expected)
        {
            SetupCriticalExceptions(new Exception[]
            {
                new NullReferenceException(),
                new InvalidCastException()
            });
            var exception = (Exception)Activator.CreateInstance(exceptionType);

            var actual = _exceptionManager.IsCriticalException(exception);

            Assert.AreEqual(expected, actual);
        }

        private void SetupCriticalExceptions(IEnumerable<Exception> exceptions)
        {
            _options.Setup(o => o.CriticalExceptionTypes)
                .Returns(exceptions.Select(e => e.GetType().ToString()).ToList());
            _exceptionManager.Options = _options.Object;
        }

        private void SetupCriticalException(Exception exception)
        {
            SetupCriticalExceptions(new[] { exception });
        }
    }
}
