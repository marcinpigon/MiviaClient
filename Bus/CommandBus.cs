using MiviaMaui.Command_Handlers;
using MiviaMaui.Commands;
using MiviaMaui.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Bus
{
    public class CommandBus : ICommandBus
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> SendAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand
        {
            var handler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand, TResult>)) as ICommandHandler<TCommand, TResult>;
            if (handler == null)
                throw new InvalidOperationException($"No handler found for {typeof(TCommand).Name}");

            return await handler.HandleAsync(command);
        }
    }
}
