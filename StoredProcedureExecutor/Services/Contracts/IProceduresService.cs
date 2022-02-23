using StoredProcedureExecutor.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Services.Contracts
{
    public interface IProceduresService
    {
        Task<ProcedureDto> CreateProcedureAsync(ProcedureDto createProcedureDto,
            IEnumerable<ParamDto>? createParamDtoList);

        Task<IEnumerable<ProcedureDto>> GetProcedures(ProcedureFilterDto? filter = null);
        Task RemoveProcedure(int procedureId);
        Task<IEnumerable<ParamDto>?> GetProcedureParams(int procedureId);
        Task<ProcedureDto> GetProcedureById(int procedureId);
        Task UpdateProcedure(ProcedureDto procedureDto, IEnumerable<ParamDto>? paramDtos);
    }
}