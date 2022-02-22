using StoredProcedureExecutor.Dtos;
using StoredProcedureExecutor.Enums;
using StoredProcedureExecutor.Models;
using StoredProcedureExecutor.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Services.Implementations
{
    public class ExecStatisticsService : IExecStatisticsService
    {
        private readonly IProceduresDbContext _context;

        public ExecStatisticsService(IProceduresDbContext context)
        {
            _context = context;
        }

        public async Task SaveProcedureExecuteInfo(TimerDto timer, ProcedureDto procedure,
            IEnumerable<ParamDto>? paramDtoList)
        {
            var paramsJson = paramDtoList != null
                ? JsonSerializer.Serialize(paramDtoList.Select(p => new { p.Name, p.Value }))
                : null;
            var statistic = BuildStatistic(OperationType.Procedure, timer, procedure, paramsJson);
            await SaveStatistic(statistic);
        }

        public async Task SaveReportRefreshInfo(TimerDto timer, ProcedureDto procedure, string templateName)
        {
            var paramsJson = JsonSerializer.Serialize(new { TemplateName = templateName });
            var statistic = BuildStatistic(OperationType.RefreshReport, timer, procedure, paramsJson);
            await SaveStatistic(statistic);
        }

        public async Task SaveReportSendInfo(TimerDto timer, ProcedureDto procedure)
        {
            var paramsJson = JsonSerializer.Serialize(new { procedure.EmailRecipients, procedure.EmailSubject });
            var statistic = BuildStatistic(OperationType.SendReport, timer, procedure, paramsJson);
            await SaveStatistic(statistic);
        }

        private static ExecStatistic BuildStatistic(OperationType operation, TimerDto timer, ProcedureDto procedure,
            string? paramsJson)
        {
            return new ExecStatistic
            {
                Username = procedure.LastUsername,
                Procedure = $"{procedure.Schema}.{procedure.Name}",
                OperationType = operation,
                StartAt = timer.StartAt,
                TimeElpsed = timer.TimeElapsed,
                ParamsJson = paramsJson,
            };
        }

        private async Task SaveStatistic(ExecStatistic statistic)
        {
            await _context.Statistics.AddAsync(statistic);
            await _context.SaveChangesAsync();
        }
    }
}