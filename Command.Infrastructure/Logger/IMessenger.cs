//  -----------------------------------------------------------------------
//  <copyright file="IMessenger.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>02/05/2016 21:47</created>
//  <modified>10/06/2016 15:11</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Logger
{
    using Logs;

    public interface IMessenger
    {
        bool HasError { get; }

        Messenger.Messages Logs { get; }

        void AddLog<TValue>(Log<TValue> log);
    }
}