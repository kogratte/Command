//  -----------------------------------------------------------------------
//  <copyright file="ValidationLogShould.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>02/05/2016 21:47</created>
//  <modified>10/06/2016 15:11</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Tests.Logs
{
    using System.ComponentModel.DataAnnotations;

    using Infrastructure.Logs;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidationLogShould
    {
        [TestMethod]
        public void BeOfTypeLogValidationResult()
        {
            ValidationLog validationLog = new ValidationLog("ValidationMessage.", "PropertyName");
            Assert.IsInstanceOfType(validationLog, typeof(Log<ValidationResult>));
        }

        [TestMethod]
        public void ReturnValidationResultWhenCallConstructorWithStringMessageAndPropertyName()
        {
            ValidationLog validationLog = new ValidationLog("ValidationMessage.", "PropertyName");
            Assert.AreEqual("ValidationMessage.", validationLog.Value.ToString());
        }

        [TestMethod]
        public void ReturnValidationResultWhenCallConstructorWithValidationResult()
        {
            ValidationLog validationLog =
                new ValidationLog(new ValidationResult("ValidationMessage.", new[] { "PropertyName" }));
            Assert.AreEqual("ValidationMessage.", validationLog.Value.ToString());
        }
    }
}