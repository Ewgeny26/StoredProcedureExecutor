using Microsoft.EntityFrameworkCore;
using StoredProcedureExecutor.Dtos;
using StoredProcedureExecutor.Exceptions;
using StoredProcedureExecutor.Mappers;
using StoredProcedureExecutor.Models;
using StoredProcedureExecutor.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Services.Implementations
{
    public class ProceduresService : IProceduresService
    {
        private readonly IProceduresDbContext _context;

        public ProceduresService(IProceduresDbContext context)
        {
            _context = context;
        }

        public async Task<ProcedureDto> CreateProcedureAsync(ProcedureDto createProcedureDto,
            IEnumerable<ParamDto>? createParamDtoList)
        {
            await CheckExistProcedure(createProcedureDto);
            var procedure = createProcedureDto.ToModel();
            procedure.CreatedAt = DateTime.UtcNow;
            await _context.Procedures.AddAsync(procedure);
            if (createParamDtoList != null)
            {
                var procedureParams = new List<ProcedureParam>();
                foreach (var param in createParamDtoList)
                {
                    var procedureParam = param.ToModel(procedure);
                    procedureParam.CreatedAt = DateTime.UtcNow;
                    procedureParams.Add(procedureParam);
                }

                await _context.ProcedureParams.AddRangeAsync(procedureParams);
            }

            await _context.SaveChangesAsync();
            return procedure.ToDto();
        }

        public async Task<ProcedureDto> GetProcedureById(int procedureId)
        {
            var procedure = await _context.Procedures
                                .FindAsync(procedureId)
                            ?? throw new EntityNotFoundException($"Procedure with id [{procedureId}] not found");
            return procedure.ToDto();
        }

        public async Task<IEnumerable<ParamDto>?> GetProcedureParams(int procedureId)
        {
            return await _context.ProcedureParams
                .Where(p => p.ProcedureId == procedureId)
                .Select(param => param.ToDto())
                .ToListAsync();
        }

        public async Task<IEnumerable<ProcedureDto>> GetProcedures(ProcedureFilterDto? filter = null)
        {
            var query = _context.Procedures.AsQueryable();
            if (filter != null && !string.IsNullOrWhiteSpace(filter.WordToFind))
            {
                query = query.Where(p => EF.Functions.Like(p.Name, $"%{filter.WordToFind}%"));
            }

            return await query
                .OrderByDescending(p=>p.CreatedAt)
                .Select(p => p.ToDto())
                .ToListAsync();
        }

        public async Task RemoveProcedure(int procedureId)
        {
            var procedure = await GetProcedureById(procedureId);
            _context.Procedures.Remove(procedure.ToModel());
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProcedure(ProcedureDto procedureDto, IEnumerable<ParamDto>? paramDtos)
        {
            var procedure = procedureDto.ToModel();
            _context.Procedures.Update(procedure);
            var existParams = await GetProcedureParams(procedure.Id);
            var existParamList = existParams?.ToList();
            if (existParamList is { Count: > 0 })
            {
                var paramModel = existParamList.Select(p => p.ToModel(procedure));
                _context.ProcedureParams.RemoveRange(paramModel);
            }

            var paramList = paramDtos?.ToList();
            if (paramList?.Count > 0)
            {
                await _context.ProcedureParams.AddRangeAsync(paramList.Select(p => p.ToModel(procedure)));
            }

            await _context.SaveChangesAsync();
        }

        private async Task CheckExistProcedure(IProcedureInfo procedureDto)
        {
            var procedureCount = await _context.Procedures
                .Where(p => p.Database == procedureDto.Database && p.Schema == procedureDto.Schema &&
                            p.Name == procedureDto.Name)
                .CountAsync();
            if (procedureCount > 0)
            {
                throw new ProcedureAlreadyExistException(
                    $"Procedure [{procedureDto.Schema}.{procedureDto.Name}] already exist");
            }
        }
    }
}