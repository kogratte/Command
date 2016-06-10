//  -----------------------------------------------------------------------
//  <copyright file="CommandIn.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>02/05/2016 21:47</created>
//  <modified>10/06/2016 15:11</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Core
{
    using System;

    using Logger;

    public abstract class CommandIn<TIn> : CommandBase, ICommandIn<TIn>
    {
        private readonly IInputValidator<TIn> inputValidator;

        public TIn Input { protected get; set; }

        public sealed override void Execute()
        {
            if (this.inputValidator.Validate(this.Input))
            {
                this.OnExecute(this.Input);
            }
        }

        protected CommandIn(IMessenger messenger, IInputValidator<TIn> inputValidator)
            : base(messenger)
        {
            bool isInstanceOf = inputValidator is InputValidator<TIn>;
            if (!isInstanceOf)
            {
                throw new ArgumentException("Le validateur doit être de type InputValidator<T>.");
            }

            this.inputValidator = inputValidator;
        }

        protected CommandIn(IMessenger messenger)
            : this(messenger, new InputValidator<TIn>(messenger))
        {
        }

        protected abstract void OnExecute(TIn input);
    }

    public interface ICommandIn<in TIn> : ICommand
    {
        TIn Input { set; }
    }
}