//  -----------------------------------------------------------------------
//  <copyright file="SuccessLogShould.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>02/05/2016 21:47</created>
//  <modified>10/06/2016 15:11</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Tests.Logs
{
    using Infrastructure.Logs;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SuccessLogShould
    {
        [TestMethod]
        public void BeOfTypeLogString()
        {
            SuccessLog successLog = new SuccessLog("SuccessMessage");
            Assert.IsInstanceOfType(successLog, typeof(Log<string>));
        }

        [TestMethod]
        public void ReturnMessageWhenCallConstructorWithStringMessage()
        {
            SuccessLog successLog = new SuccessLog("SuccessMessage.");
            Assert.AreEqual("SuccessMessage.", successLog.Value);
        }
    }
}