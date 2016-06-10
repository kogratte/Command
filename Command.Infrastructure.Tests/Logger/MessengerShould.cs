//  -----------------------------------------------------------------------
//  <copyright file="MessengerShould.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>02/05/2016 21:47</created>
//  <modified>10/06/2016 15:37</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Tests.Logger
{
    using System;
    using System.Linq;

    using Infrastructure.Logger;
    using Infrastructure.Logs;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Newtonsoft.Json;

    [TestClass]
    public class MessengerShould
    {
        [TestMethod]
        public void CallLoggerWhenAddLogIsCalled()
        {
            Mock<LogHandler> loggerMock = new Mock<LogHandler>();
            IMessenger messenger = new Messenger(loggerMock.Object);
            messenger.AddLog(new ValidationLog("validation message", "propertyName"));
            messenger.AddLog(new ErrorLog("error message"));
            messenger.AddLog(new SuccessLog("success message"));
            messenger.AddLog(new WarnLog("success message"));
            loggerMock.Verify(
                l =>
                l(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Exactly(4));
        }

        [TestMethod]
        public void Have2ValidationLogInLogsWhenCallAddLogWithValidationObject2xWithSamePropertyName()
        {
            Mock<LogHandler> loggerMock = new Mock<LogHandler>();
            IMessenger messenger = new Messenger(loggerMock.Object);
            messenger.AddLog(new ValidationLog("validation message", "property name"));
            messenger.AddLog(new ValidationLog("validation message 2", "property name"));
            var jsonValue = JsonConvert.SerializeObject(messenger.Logs.Validation.Single());
            Assert.AreEqual("{\"property name\":[\"validation message\",\"validation message 2\"]}", jsonValue);
        }

        [TestMethod]
        public void HaveErrorLogInLogsWhenCallAddLogWithErrorObject()
        {
            Mock<LogHandler> loggerMock = new Mock<LogHandler>();
            IMessenger messenger = new Messenger(loggerMock.Object);
            messenger.AddLog(new ErrorLog("error message"));
            Assert.AreEqual("error message", messenger.Logs.Error.Single());
        }

        [TestMethod]
        public void HaveLoggerCategoryEqualToErrorWhenCallAddLogWithErrorLog()
        {
            Mock<LogHandler> loggerMock = new Mock<LogHandler>();
            IMessenger messenger = new Messenger(loggerMock.Object);

            messenger.AddLog(new ErrorLog("error message"));

            loggerMock.Verify(l => l("Error", It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void HaveLoggerCategoryEqualToSuccessWhenCallAddLogWithSuccessLog()
        {
            Mock<LogHandler> loggerMock = new Mock<LogHandler>();
            IMessenger messenger = new Messenger(loggerMock.Object);
            
            messenger.AddLog(new SuccessLog("success message"));

            loggerMock.Verify(l => l("Success", It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void HaveLoggerCategoryEqualToValidationWhenCallAddLogWithValidationLog()
        {
            Mock<LogHandler> loggerMock = new Mock<LogHandler>();
            IMessenger messenger = new Messenger(loggerMock.Object);
            
            messenger.AddLog(new ValidationLog("validation message", "property"));

            loggerMock.Verify(l => l("Validation", It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void HaveLoggerCategoryEqualToWarnWhenCallAddLogWithWarnLog()
        {
            Mock<LogHandler> loggerMock = new Mock<LogHandler>();
            IMessenger messenger = new Messenger(loggerMock.Object);
            
            messenger.AddLog(new WarnLog("warn message"));

            loggerMock.Verify(l => l("Warn", It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void HaveLoggerHostNameEqualToCurrentMachineWhenCallAddLog()
        {
            Mock<LogHandler> loggerMock = new Mock<LogHandler>();
            IMessenger messenger = new Messenger(loggerMock.Object);
            
            messenger.AddLog(new ValidationLog("validation message", "property"));

            loggerMock.Verify(l => l(It.IsAny<string>(), Environment.MachineName, It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void HaveLoggerMessageEqualToLogMessageWhenCallAddLog()
        {
            Mock<LogHandler> loggerMock = new Mock<LogHandler>();
            IMessenger messenger = new Messenger(loggerMock.Object);

            messenger.AddLog(new ValidationLog("validation message", "property"));
            
            loggerMock.Verify(l => l(It.IsAny<string>(), It.IsAny<string>(), "validation message"), Times.Once);
        }

        [TestMethod]
        public void HaveSuccessLogInLogsWhenCallAddLogWithSuccessObject()
        {
            Mock<LogHandler> loggerMock = new Mock<LogHandler>();
            IMessenger messenger = new Messenger(loggerMock.Object);
            messenger.AddLog(new SuccessLog("success message"));
            Assert.AreEqual("success message", messenger.Logs.Success.Single());
        }

        [TestMethod]
        public void HaveValidationLogInLogsWhenCallAddLogWithValidationObject()
        {
            Mock<LogHandler> loggerMock = new Mock<LogHandler>();
            IMessenger messenger = new Messenger(loggerMock.Object);
            messenger.AddLog(new ValidationLog("validation message", "property name"));
            var jsonValue = JsonConvert.SerializeObject(messenger.Logs.Validation.Single());
            Assert.AreEqual("{\"property name\":[\"validation message\"]}", jsonValue);
        }

        [TestMethod]
        public void HaveWarnLogInLogsWhenCallAddLogWithWarnObject()
        {
            Mock<LogHandler> loggerMock = new Mock<LogHandler>();
            IMessenger messenger = new Messenger(loggerMock.Object);
            messenger.AddLog(new WarnLog("warn message"));
            Assert.AreEqual("warn message", messenger.Logs.Warn.Single());
        }

        [TestMethod]
        public void ReturnFalseWhenCallHasErrorAndSuccessLogHasBeenAdded()
        {
            Mock<LogHandler> loggerMock = new Mock<LogHandler>();
            IMessenger messenger = new Messenger(loggerMock.Object);
            messenger.AddLog(new SuccessLog("success message"));
            Assert.IsFalse(messenger.HasError);
        }

        [TestMethod]
        public void ReturnTrueWhenCallHasErrorAndErrorLogHasBeenAdded()
        {
            Mock<LogHandler> loggerMock = new Mock<LogHandler>();
            IMessenger messenger = new Messenger(loggerMock.Object);
            messenger.AddLog(new ErrorLog("error message"));
            Assert.IsTrue(messenger.HasError);
        }

        [TestMethod]
        public void ReturnTrueWhenCallHasErrorAndValidationLogHasBeenAdded()
        {
            Mock<LogHandler> loggerMock = new Mock<LogHandler>();
            IMessenger messenger = new Messenger(loggerMock.Object);
            messenger.AddLog(new ValidationLog("validation message", "propertyName"));
            Assert.IsTrue(messenger.HasError);
        }
    }
}