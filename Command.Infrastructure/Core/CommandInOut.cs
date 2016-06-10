//  -----------------------------------------------------------------------
//  <copyright file="CommandInOut.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>02/05/2016 21:47</created>
//  <modified>10/06/2016 15:12</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Core
{
    using System;

    using Logger;

    public abstract class CommandInOut<TIn, TOut> : CommandBase, ICommandInOut<TIn, TOut>
    {
        private readonly IInputValidator<TIn> inputValidator;

        private readonly IOutputValidator<TOut> outputValidator;

        public TIn Input { protected get; set; }

        public TOut Output { get; private set; }

        public sealed override void Execute()
        {
            if (this.inputValidator.Validate(this.Input))
            {
                this.Output = this.OnExecute(this.Input);

                this.outputValidator.Validate(this.Output);
            }
        }

        protected CommandInOut(IMessenger messenger)
            : this(messenger, new OutputValidator<TOut>(messenger))
        {
        }

        protected CommandInOut(IMessenger messenger, IOutputValidator<TOut> outputValidator)
            : this(messenger, new InputValidator<TIn>(messenger), outputValidator)
        {
        }

        protected CommandInOut(
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

        protected CommandInOut(IMessenger messenger, IInputValidator<TIn> inputValidator)
            : this(messenger, inputValidator, new OutputValidator<TOut>(messenger))
        {
        }

        protected abstract TOut OnExecute(TIn input);
    }

    public interface ICommandInOut<in TIn, out TOut> : ICommandIn<TIn>
    {
        TOut Output { get; }
    }
}