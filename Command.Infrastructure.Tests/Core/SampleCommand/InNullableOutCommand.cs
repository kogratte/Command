//  -----------------------------------------------------------------------
//  <copyright file="InNullableOutCommand.cs" company="anonyme">
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

    public class InNullableOutCommand : CommandInOut<int?, TestOutObject>
    {
        public InNullableOutCommand(IMessenger messenger)
            : base(messenger)
        {
        }

        protected override TestOutObject OnExecute(int? input)
        {
            return new TestOutObject
                       {
                           Property = "property"
                       };
        }
    }
}