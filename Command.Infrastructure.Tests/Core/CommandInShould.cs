//  -----------------------------------------------------------------------
//  <copyright file="CommandInShould.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>02/05/2016 21:47</created>
//  <modified>10/06/2016 15:34</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Tests.Core
{
    using System;
    using System.Linq;

    using Infrastructure.Logger;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CommandInShould
    {
        private IMessenger messenger;

        [TestMethod]
        public void BeNotValidWhenInputWithDataAnnotationIsNotValid()
        {
            var input = new TestInObject { Property = string.Empty };
            var inputCommand = new InDataAnnotationCommand(this.messenger) { Input = input };
            inputCommand.Execute();
            Assert.IsFalse(inputCommand.IsValid);
        }

        [TestMethod]
        public void BeNotValidWhenObjectInputIsNull()
        {
            Uri notValidInput = null;
            var inputCommand = new InObjectCommand(this.messenger) { Input = notValidInput };
            inputCommand.Execute();
            Assert.IsFalse(inputCommand.IsValid);
        }

        [TestMethod]
        public void BeNotValidWhenReplaceInputValidatorAndInputRulesIsNotValid()
        {
            var stringValidator = new StringValidator(this.messenger);
            string notValidInput = "plop";
            var inputCommand = new OverrideValidationForInCommand(this.messenger, stringValidator)
                                   {
                                       Input =
                                           notValidInput
                                   };
            inputCommand.Execute();
            Assert.IsFalse(inputCommand.IsValid);
        }

        [TestMethod]
        public void BeValidWhenInputIsPrimitive()
        {
            string validInput = It.IsAny<string>();
            var inputCommand = new InStringCommand(this.messenger) { Input = validInput };
            inputCommand.Execute();
            Assert.IsTrue(inputCommand.IsValid);
        }

        [TestMethod]
        public void BeValidWhenInputWithDataAnnotationIsValid()
        {
            var validInput = new TestInObject { Property = "value" };
            var inputCommand = new InDataAnnotationCommand(this.messenger) { Input = validInput };
            inputCommand.Execute();
            Assert.IsTrue(inputCommand.IsValid);
        }

        [TestMethod]
        public void BeValidWhenNullableInputIsNull()
        {
            int? validInput = null;
            var inputCommand = new InNullableCommand(this.messenger) { Input = validInput };
            inputCommand.Execute();
            Assert.IsTrue(inputCommand.IsValid);
        }

        [TestMethod]
        public void BeValidWhenObjectInputIsNotNull()
        {
            var validInput = new Uri("http://www.google.fr");
            var inputCommand = new InObjectCommand(this.messenger) { Input = validInput };
            inputCommand.Execute();
            Assert.IsTrue(inputCommand.IsValid);
        }

        [TestMethod]
        public void HasAErrorLogWhenObjectInputIsNull()
        {
            Uri notValidInput = null;
            var inputCommand = new InObjectCommand(this.messenger) { Input = notValidInput };
            inputCommand.Execute();
            Assert.AreEqual("L'input ne peut-être null.", inputCommand.Messenger.Logs.Error.Single());
        }

        [TestMethod]
        public void HasAValidationLogWhenInputWithDataAnnotationIsNotValid()
        {
            var notValidInput = new TestInObject { Property = string.Empty };
            var inputCommand = new InDataAnnotationCommand(this.messenger) { Input = notValidInput };
            inputCommand.Execute();
            Assert.IsTrue(inputCommand.Messenger.Logs.Validation.Any());
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
                var inputCommand = new ValidationNotInstanceOfInputValidatorCommand(
                    this.messenger,
                    new NotInstanceOfInputValidator())
                                       { Input = "plop" };
            }
            catch (ArgumentException ex)
            {
                error = ex.Message;
            }

            Assert.AreEqual(expectedError, error);
        }
    }
}