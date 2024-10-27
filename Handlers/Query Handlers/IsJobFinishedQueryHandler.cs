using MiviaMaui.Interfaces;
using MiviaMaui.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Query_Handlers
{
    public class IsJobFinishedQueryHandler : IQueryHandler<IsJobFinishedQuery, bool>
    {
        private readonly IMiviaClient _miviaClient;

        public IsJobFinishedQueryHandler(IMiviaClient miviaClient)
        {
            _miviaClient = miviaClient;
        }

        public async Task<bool> HandleAsync(IsJobFinishedQuery query)
        {
            return await _miviaClient.IsJobFinishedAsync(query.JobId);
        }
    }
}
