//  -----------------------------------------------------------------------
//  <copyright file="CommandInOutAsync.cs" company="anonyme">
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

    public abstract class CommandInOutAsync<TIn, TOut> : CommandBaseAsync, ICommandInOut<TIn, TOut>
    {
        private readonly IInputValidator<TIn> inputValidator;

        private readonly IOutputValidator<TOut> outputValidator;

        public TIn Input { protected get; set; }

        public TOut Output { get; private set; }

        public sealed override async Task Execute()
        {
            if (this.inputValidator.Validate(this.Input))
            {
                var task = this.OnExecute(this.Input) ?? Task.Factory.StartNew(() => default(TOut));

                this.Output = await task;

                this.outputValidator.Validate(this.Output);
            }
        }

        protected CommandInOutAsync(IMessenger messenger)
            : this(messenger, new OutputValidator<TOut>(messenger))
        {
        }

        protected CommandInOutAsync(IMessenger messenger, IOutputValidator<TOut> outputValidator)
            : this(messenger, new InputValidator<TIn>(messenger), outputValidator)
        {
        }

        protected CommandInOutAsync(
            IMessenger messenger,
            IInputValidator<TIn> inputValidator,
            IOutputValidator<TOut> outputValidator)
            : base(messenger)
        {
            bool isInstanceOf = inputValidator is InputValidator<TIn>;
            if (!isInstanceOf)
            {
                throw new ArgumentException("Le validateur doit être de type InputValidator<T>.");
            }

            isInstanceOf = outputValidator is OutputValidator<TOut>;
            if (!isInstanceOf)
            {
                throw new ArgumentException("Le validateur doit être de type OutputValidator<T>.");
            }

            this.inputValidator = inputValidator;
            this.outputValidator = outputValidator;
        }

        protected CommandInOutAsync(IMessenger messenger, IInputValidator<TIn> inputValidator)
            : this(messenger, inputValidator, new OutputValidator<TOut>(messenger))
        {
        }

        protected abstract Task<TOut> OnExecute(TIn input);
    }
}