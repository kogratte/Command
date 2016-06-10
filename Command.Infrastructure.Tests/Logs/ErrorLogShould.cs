//  -----------------------------------------------------------------------
//  <copyright file="ErrorLogShould.cs" company="anonyme">
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
    public class ErrorLogShould
    {
        [TestMethod]
        public void BeOfTypeLogString()
        {
            ErrorLog errorLog = new ErrorLog("VoiciMonerreur");
            Assert.IsInstanceOfType(errorLog, typeof(Log<string>));
        }

        [TestMethod]
        public void ReturnMessageWhenCallConstructorWithPatternAndManyParameters()
        {
            ErrorLog errorLog = new ErrorLog("L'id produit {0} pour le devis {1}.", "F5683", 1256);
            Assert.AreEqual("L'id produit F5683 pour le devis 1256.", errorLog.Value);
        }

        [TestMethod]
        public void ReturnMessageWhenCallConstructorWithSingleParameter()
        {
            ErrorLog errorLog = new ErrorLog("VoiciMonerreur");
            Assert.AreEqual("VoiciMonerreur", errorLog.Value);
        }
    }
}