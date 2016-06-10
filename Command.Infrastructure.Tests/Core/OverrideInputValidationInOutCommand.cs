//  -----------------------------------------------------------------------
//  <copyright file="OverrideInputValidationInOutCommand.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>02/05/2016 21:47</created>
//  <modified>10/06/2016 15:11</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Tests.Core
{
    using System;

    using Infrastructure.Core;
    using Infrastructure.Logger;

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