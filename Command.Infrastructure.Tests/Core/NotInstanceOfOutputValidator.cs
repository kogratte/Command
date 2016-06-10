//  -----------------------------------------------------------------------
//  <copyright file="NotInstanceOfOutputValidator.cs" company="anonyme">
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

    public class NotInstanceOfOutputValidator : IOutputValidator<object>
    {
        public bool Validate(object output)
        {
            throw new NotImplementedException();
        }
    }
}