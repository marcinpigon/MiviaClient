using MiviaMaui.Commands;
using MiviaMaui.Interfaces;
using MiviaMaui.Models;
using MiviaMaui.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Command_Handlers
{
    public class ScheduleJobCommandHandler : ICommandHandler<ScheduleJobCommand, string>
    {
        private readonly IMiviaClient _miviaClient;
        private readonly HistoryService _historyService;

        public ScheduleJobCommandHandler(IMiviaClient miviaClient, HistoryService historyService)
        {
            _miviaClient = miviaClient;
            _historyService = historyService;
        }

        public async Task<string> HandleAsync(ScheduleJobCommand command)
        {
            var jobId = await _miviaClient.ScheduleJobAsync(command.ImageId, command.ModelId);

            string historyMessage = jobId != null
                ? $"[{DateTime.Now}] Job scheduled successfully! Job ID: {jobId}"
                : $"[{DateTime.Now}] Failed to schedule job for Image: {command.ImageId} with Model: {command.ModelId}";

            var record = new HistoryRecord(EventType.HttpJobs, historyMessage);
            await _historyService.SaveHistoryRecordAsync(record);

            return jobId;
        }
    }
}
