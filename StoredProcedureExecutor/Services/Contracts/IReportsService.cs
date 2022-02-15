using StoredProcedureExecutor.Dtos;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Services.Contracts
{
    public interface IReportsService
    {
        Task<string> CreateReportByTemplate(TemplateDto template, string outputPath);
    }
}