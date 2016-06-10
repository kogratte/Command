//  -----------------------------------------------------------------------
//  <copyright file="InputValidator.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>02/05/2016 21:47</created>
//  <modified>10/06/2016 15:12</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Extensions;

    using Logger;

    using Logs;

    public class InputValidator<TIn> : IInputValidator<TIn>
    {
        protected IMessenger Messenger { get; }

        public InputValidator(IMessenger messenger)
        {
            this.Messenger = messenger;
        }

        public virtual bool Validate(TIn input)
        {
            Type valueType = typeof(TIn);
            bool isNull = input == null;
            if (valueType.IsPrimitive() || (isNull && valueType.IsNullableT()))
            {
                return true;
            }

            if (isNull)
            {
                this.Messenger.AddLog(new ErrorLog("L'input ne peut-être null."));
                return false;
            }

            List<ValidationResult> errors = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(input, new ValidationContext(input, null, null), errors, true);

            foreach (ValidationLog result in errors.Select(e => new ValidationLog(e)))
            {
                this.Messenger.AddLog(result);
            }

            return valid;
        }
    }

    public interface IInputValidator<in TIn>
    {
        bool Validate(TIn input);
    }
}