//  -----------------------------------------------------------------------
//  <copyright file="OutputValidator.cs" company="anonyme">
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

    public class OutputValidator<TOut> : IOutputValidator<TOut>
    {
        protected IMessenger Messenger { get; }

        public OutputValidator(IMessenger messenger)
        {
            this.Messenger = messenger;
        }

        public virtual bool Validate(TOut output)
        {
            Type valueType = typeof(TOut);
            if (valueType.IsPrimitive() || output == null)
            {
                return true;
            }

            List<ValidationResult> errors = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(output, new ValidationContext(output, null, null), errors, true);

            foreach (ValidationLog result in errors.Select(e => new ValidationLog(e)))
            {
                this.Messenger.AddLog(result);
            }

            return valid;
        }
    }

    public interface IOutputValidator<in TOut>
    {
        bool Validate(TOut input);
    }
}