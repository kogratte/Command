//  -----------------------------------------------------------------------
//  <copyright file="Program.cs" company="AXA France Service">
//      Copyright (c) AXA France Service. All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>26/02/2016 08:28</created>
//  <modified>22/03/2016 15:14</modified>
//  -----------------------------------------------------------------------

namespace Command.App
{
    using System;
    using System.Threading.Tasks;

    using Infrastructure.Core;
    using Infrastructure.Logger;

    public class Program
    {
        private static async Task<GenericInOutCommandAsync<string, string>> CallAsync()
        {
            IProcessor processor = new Processor();
            var commandAsync = await processor.ProcessAsync<GenericInOutCommandAsync<string, string>, string>("plopp");
            return commandAsync;
        }

        private static void Main(string[] args)
        {
            IProcessor processor = new Processor();
            GenericInCommand<string> command = processor.Process<GenericInCommand<string>, string>("plopp");
            CallAsync();
        }
    }

    public class GenericInCommand<TIn> : CommandIn<TIn>
    {
        public GenericInCommand(IMessenger messenger)
            : base(messenger)
        {
        }

        protected override void OnExecute(TIn input)
        {
            throw new NotImplementedException();
        }
    }

    public class GenericInOutCommandAsync<TIn, TOut> : CommandInOutAsync<TIn, TOut>
    {
        public GenericInOutCommandAsync(IMessenger messenger)
            : base(messenger)
        {
        }

        public GenericInOutCommandAsync(IMessenger messenger, IOutputValidator<TOut> outputValidator)
            : base(messenger, outputValidator)
        {
        }

        public GenericInOutCommandAsync(
            IMessenger messenger,
            IInputValidator<TIn> inputValidator,
            IOutputValidator<TOut> outputValidator)
            : base(messenger, inputValidator, outputValidator)
        {
        }

        public GenericInOutCommandAsync(IMessenger messenger, IInputValidator<TIn> inputValidator)
            : base(messenger, inputValidator)
        {
        }

        protected override Task<TOut> OnExecute(TIn input)
        {
            throw new NotImplementedException();
        }
    }
}