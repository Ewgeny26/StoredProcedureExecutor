using StoredProcedureExecutor.Dtos;
using StoredProcedureExecutor.Services.Contracts;
using System;
using System.IO;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Services.Implementations
{
    public class ReportsService : IReportsService
    {
        private readonly ITemplatesService _filesService;
        private readonly ITemplateProviderService _templateProviderService;
        public ReportsService(
            ITemplatesService filesService,
            ITemplateProviderService templateProviderService)
        {
            _filesService = filesService;
            _templateProviderService = templateProviderService;
        }
        public async Task<string> CreateReportByTemplate(TemplateDto template, string outputPath)
        {
            var outputFilePath = await _filesService.DownloadFileAsync(template, BuildReportName(template), outputPath);
            await _templateProviderService.RefreshDataAsync(outputFilePath);
            return outputFilePath;
        }

        private string BuildReportName(TemplateDto template)
        {
            var date = DateTime.Now.ToString("yyyyMMddHHmmss");
            return $"{Path.GetFileNameWithoutExtension(template.Name)}_{date}";
        }
    }
}
