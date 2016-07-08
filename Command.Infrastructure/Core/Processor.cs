//  -----------------------------------------------------------------------
//  <copyright file="Processor.cs" company="anonyme">
//      Copyright (c) . All rights reserved.
//  </copyright>
//  <actor>S614599 (VANDENBUSSCHE Julien)</actor>
//  <created>02/05/2016 21:47</created>
//  <modified>10/06/2016 15:12</modified>
//  -----------------------------------------------------------------------

namespace Command.Infrastructure.Core
{
    using System.Threading.Tasks;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    public sealed class Processor : IProcessor
    {
        private readonly IUnityContainer container;
        
        public Processor(IUnityContainer container)
        {
            this.container = container;
        }

        public TCommand Process<TCommand, TIn>(TIn input) where TCommand : ICommandIn<TIn>
        {
            TCommand command = this.container.Resolve<TCommand>();
            command.Input = input;
            var commandBase = command as CommandBase;
            commandBase.Execute();
            return command;
        }

        public async Task<TCommand> ProcessAsync<TCommand, TIn>(TIn input)
            where TCommand : ICommandIn<TIn>
        {
            TCommand command = this.container.Resolve<TCommand>();
            command.Input = input;
            var commandBase = command as CommandBaseAsync;
            await commandBase.Execute();
            return command;
        }
    }

    public interface IProcessor
    {
        TCommand Process<TCommand, TIn>(TIn input) where TCommand : ICommandIn<TIn>;

        Task<TCommand> ProcessAsync<TCommand, TIn>(TIn input) where TCommand : ICommandIn<TIn>;
    }
}