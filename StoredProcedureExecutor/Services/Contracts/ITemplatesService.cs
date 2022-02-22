using StoredProcedureExecutor.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Services.Contracts
{
    public interface ITemplatesService
    {
        Task Upload(string path, int procedureId);
        Task<List<TemplateDto>> GetAll();
        Task Remove(int templateId);
        Task<TemplateDto?> GetByProcedureId(int procedureId);
        Task<string> DownloadFileAsync(TemplateDto template, string fileName, string outputPath);
        void CheckExistTemplate(string path);
    }
}