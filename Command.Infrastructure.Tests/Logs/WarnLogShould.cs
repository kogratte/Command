//  -----------------------------------------------------------------------
//  <copyright file="WarnLogShould.cs" company="anonyme">
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
    public class WarnLogShould
    {
        [TestMethod]
        public void BeOfTypeLogString()
        {
            WarnLog warnLog = new WarnLog("WarnMessage.");
            Assert.IsInstanceOfType(warnLog, typeof(Log<string>));
        }

        [TestMethod]
        public void ReturnMessageWhenCallConstructorWithStringMessage()
        {
            WarnLog warnLog = new WarnLog("WarnMessage.");
            Assert.AreEqual("WarnMessage.", warnLog.Value);
        }
    }
}