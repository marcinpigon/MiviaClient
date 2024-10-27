using MiviaMaui.Command_Handlers;
using MiviaMaui.Commands;
using MiviaMaui.Interfaces;
using MiviaMaui.Models;
using MiviaMaui.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Handlers.Command_Handlers
{
    public class GenerateReportMultipleJobsHandler : ICommandHandler<GenerateReportMultipleJobsCommand, bool>
    {
        private readonly IMiviaClient _miviaClient;
        private readonly HistoryService _historyService;

        public GenerateReportMultipleJobsHandler(IMiviaClient miviaClient, HistoryService historyService)
        {
            _miviaClient = miviaClient;
            _historyService = historyService;
        }

        public async Task<bool> HandleAsync(GenerateReportMultipleJobsCommand command)
        {
            try
            {
                await _miviaClient.GetSaveReportsPDF(command.JobIds, command.OutputPath);

                var historyMessage = $"[{DateTime.Now}] Report generated for Job IDs: {command.JobIds}, saved at: {command.OutputPath}";
                var record = new HistoryRecord(EventType.HttpJobs, historyMessage);
                await _historyService.SaveHistoryRecordAsync(record);

                return true;
            }
            catch (Exception ex)
            {
                var historyMessage = $"[{DateTime.Now}] Failed to generate report for Job IDs: {command.JobIds}. Error: {ex.Message}";
                var record = new HistoryRecord(EventType.HttpJobs, historyMessage);
                await _historyService.SaveHistoryRecordAsync(record);

                return false;
            }
        }
    }
}
