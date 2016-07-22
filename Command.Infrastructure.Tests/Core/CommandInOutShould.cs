//  -----------------------------------------------------------------------
//  <copyright file="CommandInOutShould.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>10/06/2016 15:49</created>
//  <modified>10/06/2016 16:15</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Tests.Core
{
    using System;
    using System.Linq;

    using Infrastructure.Core;
    using Infrastructure.Logger;

    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using SampleCommand;

    using SampleValidator;

    [TestClass]
    public class CommandInOutShould
    {
        private Messenger messenger;

        [TestMethod]
        public void BeNotValidWhenInputWithDataAnnotationIsNotValid()
        {
            var notValidInput = new TestInObject { Property = string.Empty };
            var inOutCommand = new InDataAnnotationOutCommand(this.messenger) { Input = notValidInput };
            inOutCommand.Execute();
            Assert.IsFalse(inOutCommand.IsValid);
        }

        [TestMethod]
        public void BeNotValidWhenObjectInputIsNull()
        {
            TestInObject notValidInput = null;
            var inOutCommand = new InDataAnnotationOutCommand(this.messenger) { Input = notValidInput };
            inOutCommand.Execute();
            Assert.IsFalse(inOutCommand.IsValid);
        }

        [TestMethod]
        public void BeNotValidWhenOutputObjectIsIncorrect()
        {
            TestInObject validInput = new TestInObject { Property = "property" };
            var inOutCommand = new OutputNotValidInOutCommand(this.messenger) { Input = validInput };
            inOutCommand.Execute();
            Assert.IsFalse(inOutCommand.IsValid);
        }

        [TestMethod]
        public void BeNotValidWhenOverrideOnValidateInputAndInputRulesIsNotValid()
        {
            string notValidInput = "plop";
            var inOutCommand = new OverrideInValidationForInOutCommand(this.messenger, new StringValidator(this.messenger)) { Input = notValidInput };
            inOutCommand.Execute();
            Assert.IsFalse(inOutCommand.IsValid);
        }

        [TestMethod]
        public void BeValidWhenInputIsPrimitiveType()
        {
            string validInput = "plopp";
            var inOutCommand = new InStringOutNullValueCommand(this.messenger) { Input = validInput };
            inOutCommand.Execute();
            Assert.IsTrue(inOutCommand.IsValid);
        }

        [TestMethod]
        public void BeValidWhenInputWithDataAnnotationIsValid()
        {
            var validInput = new TestInObject { Property = "value" };
            var inOutCommand = new InDataAnnotationOutCommand(this.messenger) { Input = validInput };
            inOutCommand.Execute();
            Assert.IsTrue(inOutCommand.IsValid);
        }

        [TestMethod]
        public void BeValidWhenNullableInputIsNull()
        {
            int? validInput = null;
            var inOutCommand = new InNullableOutCommand(this.messenger) { Input = validInput };
            inOutCommand.Execute();
            Assert.IsTrue(inOutCommand.IsValid);
        }

        [TestMethod]
        public void BeValidWhenObjectInputIsNotNull()
        {
            var validInput = new TestInObject { Property = "plop" };
            var inOutCommand = new InDataAnnotationOutCommand(this.messenger) { Input = validInput };
            inOutCommand.Execute();
            Assert.IsTrue(inOutCommand.IsValid);
        }

        [TestMethod]
        public void BeValidWhenOutputIsNull()
        {
            string input = It.IsAny<string>();
            var inOutCommand = new InStringOutNullValueCommand(this.messenger) { Input = input };
            inOutCommand.Execute();
            Assert.IsTrue(inOutCommand.IsValid);
        }

        [TestMethod]
        public void BeValidWhenStringInputIsNull()
        {
            string validInput = null;
            var inOutCommand = new InStringOutNullValueCommand(this.messenger) { Input = validInput };
            inOutCommand.Execute();
            Assert.IsTrue(inOutCommand.IsValid);
        }

        [TestMethod]
        public void HasAErrorLogWhenObjectInputIsNull()
        {
            TestInObject notValidInput = null;
            var inOutCommand = new InDataAnnotationOutCommand(this.messenger) { Input = notValidInput };
            inOutCommand.Execute();
            Assert.AreEqual("L'input ne peut-être null.", inOutCommand.Messenger.Logs.Error.Single());
        }

        [TestMethod]
        public void HasAValidationLogWhenInputWithDataAnnotationIsNotValid()
        {
            var notValidInput = new TestInObject { Property = string.Empty };
            var inOutCommand = new InDataAnnotationOutCommand(this.messenger) { Input = notValidInput };
            inOutCommand.Execute();
            Assert.IsTrue(inOutCommand.Messenger.Logs.Validation.Any());
        }

        [TestInitialize]
        public void Initialize()
        {
            this.messenger = new Messenger(new Mock<LogHandler>().Object);
        }

        [TestMethod]
        public void ThrowArgumentExceptionWhenValidatorIsNotInstanceOfInputValidator()
        {
            string expectedError = "Le validateur doit être de type InputValidator<T>.";
            string error = string.Empty;
            try
            {
                new ValidationNotInstanceOfInputValidatorInOutCommand(this.messenger, new NotInstanceOfInputValidator());
            }
            catch (ArgumentException ex)
            {
                error = ex.Message;
            }

            Assert.AreEqual(expectedError, error);
        }

        [TestMethod]
        public void ThrowArgumentExceptionWhenValidatorIsNotInstanceOfOutputValidator()
        {
            string expectedError = "Le validateur doit être de type OutputValidator<T>.";
            string error = string.Empty;
            try
            {
                new ValidationNotInstanceOfOutputValidatorInOutCommand(this.messenger, new NotInstanceOfOutputValidator());
            }
            catch (ArgumentException ex)
            {
                error = ex.Message;
            }

            Assert.AreEqual(expectedError, error);
        }
    }
}