using StoredProcedureExecutor.Dtos;
using StoredProcedureExecutor.Models;

namespace StoredProcedureExecutor.Mappers
{
    public static class ProcedureMapperExtenstions
    {
        public static ProcedureDto ToDto(this Procedure model)
        {
            return new ProcedureDto
            {
                Id = model.Id,
                Database = model.Database,
                Server = model.Server,
                Schema = model.Schema,
                Name = model.Name,
                Description = model.Description,
                EmailRecipients = model.EmailRecipients,
                EmailSubject = model.EmailSubject,
                OutputReportPath = model.OutputReportPath,
                LastExecutedAt = model.LastExecutedAt,
                LastRefreshedAt = model.LastRefreshedAt,
                LastSentTemplateAt = model.LastSentTemplateAt,
                LastUsername = model.LastUsername,
            };
        }

        public static ParamDto ToDto(this ProcedureParam model)
        {
            return new ParamDto
            {
                Id = model.Id,
                Name = model.Name,
                Alias = model.Alias,
                Type = model.Type,
                Value = model.Value
            };
        }

        public static Procedure ToModel(this ProcedureDto dto)
        {
            return new Procedure
            {
                Id = dto.Id.HasValue ? dto.Id.Value : 0,
                Database = dto.Database,
                Server = dto.Server,
                Schema = dto.Schema,
                Name = dto.Name,
                Description = dto.Description,
                EmailRecipients = dto.EmailRecipients,
                EmailSubject = dto.EmailSubject,
                OutputReportPath = dto.OutputReportPath,
                LastExecutedAt = dto.LastExecutedAt,
                LastRefreshedAt = dto.LastRefreshedAt,
                LastSentTemplateAt = dto.LastSentTemplateAt,
                LastUsername = dto.LastUsername,
            };
        }

        public static ProcedureParam ToModel(this ParamDto dto, Procedure procedure)
        {
            return new ProcedureParam
            {
                Id = dto.Id.HasValue ? dto.Id.Value : 0,
                Name = dto.Name,
                Alias = dto.Alias,
                Type = dto.Type,
                Value = dto.Value,
                Procedure = procedure
            };
        }
    }
}
