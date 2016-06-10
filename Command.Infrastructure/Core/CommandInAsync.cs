//  -----------------------------------------------------------------------
//  <copyright file="CommandInAsync.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>02/05/2016 21:47</created>
//  <modified>10/06/2016 15:11</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Core
{
    using System;
    using System.Threading.Tasks;

    using Logger;

    public abstract class CommandInAsync<TIn> : CommandBaseAsync, ICommandIn<TIn>
    {
        private readonly IInputValidator<TIn> inputValidator;

        public TIn Input { protected get; set; }

        public sealed override async Task Execute()
        {
            if (this.inputValidator.Validate(this.Input))
            {
                await this.OnExecute(this.Input);
            }
        }

        protected CommandInAsync(IMessenger messenger, IInputValidator<TIn> inputValidator)
            : base(messenger)
        {
            bool isInstanceOf = inputValidator is InputValidator<TIn>;
            if (!isInstanceOf)
            {
                throw new ArgumentException("Le validateur doit être de type InputValidator<T>.");
            }

            this.inputValidator = inputValidator;
        }

        protected CommandInAsync(IMessenger messenger)
            : this(messenger, new InputValidator<TIn>(messenger))
        {
        }

        protected abstract Task OnExecute(TIn input);
    }
}