//  -----------------------------------------------------------------------
//  <copyright file="ValidationLog.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>02/05/2016 21:47</created>
//  <modified>10/06/2016 15:11</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Logs
{
    using System.ComponentModel.DataAnnotations;

    public class ValidationLog : Log<ValidationResult>
    {
        public ValidationLog(string errorMessage, string memberName)
            : this(new ValidationResult(errorMessage, new[] { memberName }))
        {
        }

        public ValidationLog(ValidationResult validationResult)
            : base(validationResult)
        {
        }
    }
}