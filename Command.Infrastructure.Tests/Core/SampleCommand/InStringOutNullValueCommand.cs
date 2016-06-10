//  -----------------------------------------------------------------------
//  <copyright file="InStringOutNullValueCommand.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>10/06/2016 16:16</created>
//  <modified>10/06/2016 16:20</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Tests.Core.SampleCommand
{
    using Infrastructure.Core;
    using Infrastructure.Logger;

    public class InStringOutNullValueCommand : CommandInOut<string, string>
    {
        public InStringOutNullValueCommand(IMessenger messenger)
            : base(messenger)
        {
        }

        protected override string OnExecute(string input)
        {
            return null;
        }
    }
}