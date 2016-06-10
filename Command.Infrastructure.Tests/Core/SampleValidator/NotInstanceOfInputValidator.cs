//  -----------------------------------------------------------------------
//  <copyright file="NotInstanceOfInputValidator.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>10/06/2016 16:16</created>
//  <modified>10/06/2016 16:20</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Tests.Core.SampleValidator
{
    using System;

    using Infrastructure.Core;

    public class NotInstanceOfInputValidator : IInputValidator<string>
    {
        public bool Validate(string input)
        {
            throw new NotImplementedException();
        }
    }
}