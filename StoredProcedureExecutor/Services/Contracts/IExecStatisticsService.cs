using StoredProcedureExecutor.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Services.Contracts
{
    public interface IExecStatisticsService
    {
        Task SaveProcedureExecuteInfo(TimerDto timer, ProcedureDto procedure, IEnumerable<ParamDto>? paramDtos);
        Task SaveReportRefreshInfo(TimerDto timer, ProcedureDto procedure, string templateName);
        Task SaveReportSendInfo(TimerDto timer, ProcedureDto procedure);
    }
}
