using StoredProcedureExecutor.Dtos;
using StoredProcedureExecutor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Services.Contracts
{
    public interface IProceduresService
    {
        Task<ProcedureDto> CreateProcedureAsync(ProcedureDto createProcedureDto, IEnumerable<ParamDto>? createParamDtos);
        Task<IEnumerable<ProcedureDto>> GetProcedures();
        Task RemoveProcedure(int procedureId);
        Task<IEnumerable<ParamDto>?> GetProcedureParams(int procedureId);
        Task<ProcedureDto> GetProcedureById(int procedureId);
        Task UpdateProcedure(ProcedureDto procedureDto, IEnumerable<ParamDto>? paramDtos);
    }
}
