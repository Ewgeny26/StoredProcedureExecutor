using StoredProcedureExecutor.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Services.Contracts
{
    public interface IProcExecutorService
    {
        Task ExecuteProc(IProcedureInfo procedureInfo, IEnumerable<IBaseParamInfo>? paramInfo);
        Task<IEnumerable<ParamInfo>> GetProcedureParamsInfo(IProcedureInfo procedureInfo);
        List<string>? GetAvailableServers();
        Task CheckExistProcedure(IProcedureInfo procedureInfo);
    }
}
