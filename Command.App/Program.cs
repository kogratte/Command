//  -----------------------------------------------------------------------
//  <copyright file="Program.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>10/06/2016 15:49</created>
//  <modified>10/06/2016 16:19</modified>
//  -----------------------------------------------------------------------

namespace Command.App
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;

    using Infrastructure.Core;
    using Infrastructure.Logger;

    using Microsoft.Practices.Unity;

    public class Program
    {
        private static async Task<GenericInOutCommandAsync<string, string>> CallAsync(IUnityContainer container)
        {
            IProcessor processor = new Processor(container);
            var commandAsync = await processor.ProcessAsync<GenericInOutCommandAsync<string, string>, string>("plopp");
            return commandAsync;
        }

        private static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IMessenger, Messenger>();
            LogHandler logHandler = (string category, string hostName, string ùessage) => { };
            container.RegisterInstance(logHandler);

            IProcessor processor = new Processor(container);
            GenericInCommand<string> commandIn = processor.Process<GenericInCommand<string>, string>("plopp");
            var commandOut = CallAsync(container).Result;
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