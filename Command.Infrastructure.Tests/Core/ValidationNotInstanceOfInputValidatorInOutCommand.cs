//  -----------------------------------------------------------------------
//  <copyright file="ValidationNotInstanceOfInputValidatorInOutCommand.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>02/05/2016 21:47</created>
//  <modified>10/06/2016 15:11</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Tests.Core
{
    using Infrastructure.Core;
    using Infrastructure.Logger;

    public class ValidationNotInstanceOfInputValidatorInOutCommand : CommandInOut<string, object>
    {
        public ValidationNotInstanceOfInputValidatorInOutCommand(
            IMessenger messenger,
            NotInstanceOfInputValidator inputValidator)
            : base(messenger, inputValidator)
        {
        }

        protected override object OnExecute(string input)
        {
            return default(object);
        }
    }
}