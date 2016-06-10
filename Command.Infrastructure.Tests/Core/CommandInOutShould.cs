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
        private Processor processor;

        [TestMethod]
        public void BeNotValidWhenInputWithDataAnnotationIsNotValid()
        {
            var notValidInput = new TestInObject { Property = string.Empty };
            var inOutCommand = this.processor.Process<InDataAnnotationOutCommand, TestInObject>(notValidInput);
            Assert.IsFalse(inOutCommand.IsValid);
        }

        [TestMethod]
        public void BeNotValidWhenObjectInputIsNull()
        {
            TestInObject notValidInput = null;
            var inOutCommand = this.processor.Process<InDataAnnotationOutCommand, TestInObject>(notValidInput);
            Assert.IsFalse(inOutCommand.IsValid);
        }

        [TestMethod]
        public void BeNotValidWhenOutputObjectIsIncorrect()
        {
            TestInObject validInput = new TestInObject { Property = "property" };
            var inOutCommand = this.processor.Process<OutputNotValidInOutCommand, TestInObject>(validInput);
            Assert.IsFalse(inOutCommand.IsValid);
        }

        [TestMethod]
        public void BeNotValidWhenOverrideOnValidateInputAndInputRulesIsNotValid()
        {
            string notValidInput = "plop";
            var inOutCommand = this.processor.Process<OverrideInValidationForInOutCommand, string>(notValidInput);
            Assert.IsFalse(inOutCommand.IsValid);
        }

        [TestMethod]
        public void BeValidWhenInputIsPrimitiveType()
        {
            string validInput = "plopp";
            var inOutCommand = this.processor.Process<InStringOutNullValueCommand, string>(validInput);
            Assert.IsTrue(inOutCommand.IsValid);
        }

        [TestMethod]
        public void BeValidWhenInputWithDataAnnotationIsValid()
        {
            var validInput = new TestInObject { Property = "value" };
            var inOutCommand = this.processor.Process<InDataAnnotationOutCommand, TestInObject>(validInput);
            Assert.IsTrue(inOutCommand.IsValid);
        }

        [TestMethod]
        public void BeValidWhenNullableInputIsNull()
        {
            int? validInput = null;
            var inOutCommand = this.processor.Process<InNullableOutCommand, int?>(validInput);
            Assert.IsTrue(inOutCommand.IsValid);
        }

        [TestMethod]
        public void BeValidWhenObjectInputIsNotNull()
        {
            var validInput = new TestInObject { Property = "plop" };
            var inOutCommand = this.processor.Process<InDataAnnotationOutCommand, TestInObject>(validInput);
            Assert.IsTrue(inOutCommand.IsValid);
        }

        [TestMethod]
        public void BeValidWhenOutputIsNull()
        {
            string input = It.IsAny<string>();
            var inOutCommand = this.processor.Process<InStringOutNullValueCommand, string>(input);
            Assert.IsTrue(inOutCommand.IsValid);
        }

        [TestMethod]
        public void BeValidWhenStringInputIsNull()
        {
            string validInput = null;
            var inOutCommand = this.processor.Process<InStringOutNullValueCommand, string>(validInput);
            Assert.IsTrue(inOutCommand.IsValid);
        }

        [TestMethod]
        public void HasAErrorLogWhenObjectInputIsNull()
        {
            TestInObject notValidInput = null;
            var inOutCommand = this.processor.Process<InDataAnnotationOutCommand, TestInObject>(notValidInput);
            Assert.AreEqual("L'input ne peut-être null.", inOutCommand.Messenger.Logs.Error.Single());
        }

        [TestMethod]
        public void HasAValidationLogWhenInputWithDataAnnotationIsNotValid()
        {
            var notValidInput = new TestInObject { Property = string.Empty };
            var inOutCommand = this.processor.Process<InDataAnnotationOutCommand, TestInObject>(notValidInput);
            Assert.IsTrue(inOutCommand.Messenger.Logs.Validation.Any());
        }

        [TestInitialize]
        public void Initialize()
        {
            var messenger = new Messenger(new Mock<LogHandler>().Object);
            Mock<IUnityContainer> container = new Mock<IUnityContainer>();
            container.Setup(s => s.Resolve<InDataAnnotationOutCommand>())
                     .Returns(() => new InDataAnnotationOutCommand(messenger));

            container.Setup(s => s.Resolve<OutputNotValidInOutCommand>())
                     .Returns(() => new OutputNotValidInOutCommand(messenger));

            container.Setup(s => s.Resolve<OverrideInValidationForInOutCommand>())
                     .Returns(
                         () => new OverrideInValidationForInOutCommand(messenger, new StringValidator(messenger)));

            container.Setup(s => s.Resolve<InStringOutNullValueCommand>())
                     .Returns(() => new InStringOutNullValueCommand(messenger));

            container.Setup(s => s.Resolve<InNullableOutCommand>())
                     .Returns(() => new InNullableOutCommand(messenger));

            container.Setup(s => s.Resolve<ValidationNotInstanceOfInputValidatorInOutCommand>())
                     .Returns(
                         () =>
                         new ValidationNotInstanceOfInputValidatorInOutCommand(
                             messenger,
                             new NotInstanceOfInputValidator()));

            container.Setup(s => s.Resolve<ValidationNotInstanceOfOutputValidatorInOutCommand>())
                     .Returns(
                         () =>
                         new ValidationNotInstanceOfOutputValidatorInOutCommand(
                             messenger,
                             new NotInstanceOfOutputValidator()));

            this.processor = new Processor(container.Object);
        }

        [TestMethod]
        public void ThrowArgumentExceptionWhenValidatorIsNotInstanceOfInputValidator()
        {
            string expectedError = "Le validateur doit être de type InputValidator<T>.";
            string error = string.Empty;
            try
            {
                this.processor.Process<ValidationNotInstanceOfInputValidatorInOutCommand, string>(
                    It.IsAny<string>());
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
                this.processor.Process<ValidationNotInstanceOfOutputValidatorInOutCommand, string>(
                    It.IsAny<string>());
            }
            catch (ArgumentException ex)
            {
                error = ex.Message;
            }

            Assert.AreEqual(expectedError, error);
        }
    }
}