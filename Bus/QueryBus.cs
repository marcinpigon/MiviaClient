using MiviaMaui.Handlers;
using MiviaMaui.Queries;
using MiviaMaui.Query_Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Bus
{
    public class QueryBus : IQueryBus
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var handler = _serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResult>)) as IQueryHandler<TQuery, TResult>;
            if (handler == null)
                throw new InvalidOperationException($"No handler found for {typeof(TQuery).Name}");

            return await handler.HandleAsync(query);
        }
    }
}
