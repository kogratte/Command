//  -----------------------------------------------------------------------
//  <copyright file="CommandBase.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>02/05/2016 21:47</created>
//  <modified>10/06/2016 15:11</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Core
{
    using Logger;

    public abstract class CommandBase
    {
        public bool IsValid
        {
            get
            {
                return !this.Messenger.HasError;
            }
        }

        public IMessenger Messenger { get; }

        public abstract void Execute();

        protected CommandBase(IMessenger messenger)
        {
            this.Messenger = messenger;
        }
    }

    public interface ICommand
    {
        bool IsValid { get; }

        IMessenger Messenger { get; }
    }
}