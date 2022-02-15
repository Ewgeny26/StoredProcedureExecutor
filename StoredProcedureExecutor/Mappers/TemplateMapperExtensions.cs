using StoredProcedureExecutor.Dtos;
using StoredProcedureExecutor.Models;

namespace StoredProcedureExecutor.Mappers
{
    public static class TemplateMapperExtensions
    {
        public static TemplateDto ToDto(this Template model)
        {
            return new TemplateDto
            {
                Id = model.Id,
                Name = model.Name,
                FileType = model.FileType,
                ProcedureId = model.ProcedureId
            };
        }

        public static Template ToModel(this TemplateDto dto)
        {
            return new Template
            {
                Id = dto.Id.HasValue ? dto.Id.Value : 0,
                Name = dto.Name,
                FileType = dto.FileType,
                ProcedureId = dto.ProcedureId
            };
        }
    }
}
