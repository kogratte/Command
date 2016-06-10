//  -----------------------------------------------------------------------
//  <copyright file="StringValidator.cs" company="anonyme">
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
    using Infrastructure.Logs;

    public class StringValidator : InputValidator<string>
    {
        public StringValidator(IMessenger messenger)
            : base(messenger)
        {
        }

        public override bool Validate(string input)
        {
            bool valid = base.Validate(input);
            if (valid && input.Length <= 4)
            {
                this.Messenger.AddLog(new ValidationLog("Probleme de taille", nameof(input)));
                return false;
            }

            return valid;
        }
    }
}