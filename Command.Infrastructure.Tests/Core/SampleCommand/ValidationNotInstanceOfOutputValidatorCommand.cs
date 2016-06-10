//  -----------------------------------------------------------------------
//  <copyright file="ValidationNotInstanceOfOutputValidatorCommand.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>10/06/2016 16:16</created>
//  <modified>10/06/2016 16:20</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Tests.Core.SampleCommand
{
    using Infrastructure.Core;
    using Infrastructure.Logger;

    using SampleValidator;

    public class ValidationNotInstanceOfOutputValidatorInOutCommand : CommandInOut<string, object>
    {
        public ValidationNotInstanceOfOutputValidatorInOutCommand(
            IMessenger messenger,
            NotInstanceOfOutputValidator outputValidator)
            : base(messenger, outputValidator)
        {
        }

        protected override object OnExecute(string input)
        {
            return default(object);
        }
    }
}