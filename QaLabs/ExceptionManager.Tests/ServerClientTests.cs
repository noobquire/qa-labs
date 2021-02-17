using System;
using ExceptionManager.Implementations;
using ExceptionManager.Interfaces;
using Moq;
using NUnit.Framework;

namespace ExceptionManager.Tests
{
    [TestFixture]
    public class ServerClientTests
    {
        private IServerClient _serverClient;
        private Mock<IWebClient> _webClient;

        [SetUp]
        public void Setup()
        {
            _webClient = new Mock<IWebClient>();
            _serverClient = new ServerClient(_webClient.Object);
        }

        [Test]
        public void SendExceptionData_When_ErrorSendingData_Increments_ServerErrorCount()
        {
            _webClient.Setup(c => c.SendData(It.IsAny<string>()))
                .Throws<WebClientException>();
            var expectedErrorCount = 1;

            _serverClient.SendExceptionData(new ApplicationException());
            var actualErrorCount = _serverClient.ServerErrorCount;

            Assert.AreEqual(expectedErrorCount, actualErrorCount);
        }

        [Test]
        public void SendExceptionData_When_ErrorSendingData_Returns_False()
        {
            _webClient.Setup(c => c.SendData(It.IsAny<string>()))
                .Throws<WebClientException>();

            var actual = _serverClient.SendExceptionData(new ApplicationException());

            Assert.IsFalse(actual);
        }

        [Test]
        public void SendExceptionData_When_SuccessfullySentData_Returns_True()
        {
            var actual = _serverClient.SendExceptionData(new ApplicationException());

            Assert.IsTrue(actual);
        }

        [Test]
        public void SendExceptionData_When_SuccessfulCall_Invokes_WebClient_SendData()
        {
            _serverClient.SendExceptionData(new Exception());
            _webClient.Verify(wc => wc.SendData(It.IsAny<string>()), Times.Once);
        }
    }
}
