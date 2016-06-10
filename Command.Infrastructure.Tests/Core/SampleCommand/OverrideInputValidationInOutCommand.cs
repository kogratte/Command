//  -----------------------------------------------------------------------
//  <copyright file="OverrideInputValidationInOutCommand.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>10/06/2016 16:16</created>
//  <modified>10/06/2016 16:20</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Tests.Core.SampleCommand
{
    using System;

    using Infrastructure.Core;
    using Infrastructure.Logger;

    using SampleValidator;

    public class OverrideInValidationForInOutCommand : CommandInOut<string, TestOutObject>
    {
        public OverrideInValidationForInOutCommand(IMessenger messenger, StringValidator inputValidator)
            : base(messenger, inputValidator)
        {
        }

        protected override TestOutObject OnExecute(string input)
        {
            throw new NotImplementedException();
        }
    }
}