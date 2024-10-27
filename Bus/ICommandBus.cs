using MiviaMaui.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Bus
{
    public interface ICommandBus
    {
        Task<TResult> SendAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand;
    }
}
