using Microsoft.EntityFrameworkCore;
using StoredProcedureExecutor.Dtos;
using StoredProcedureExecutor.Exceptions;
using StoredProcedureExecutor.Mappers;
using StoredProcedureExecutor.Models;
using StoredProcedureExecutor.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Services.Implementations
{
    public class DbTemplatesService : ITemplatesService
    {
        private readonly IProceduresDbContext _proceduresDbContext;
        public DbTemplatesService(IProceduresDbContext proceduresDbContext)
        {
            _proceduresDbContext = proceduresDbContext;
        }

        public async Task Upload(string path, int procedureId)
        {
            CheckExistTemplate(path);
            var template = new Template
            {
                Name = Path.GetFileName(path),
                CreatedAt = DateTime.Now,
                FileType = Path.GetExtension(path),
                ProcedureId = procedureId,
            };

            using (var file = File.Open(path, FileMode.Open))
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    template.DataFile = memoryStream.ToArray();
                }
            }
            await _proceduresDbContext.Templates.AddAsync(template);
            await _proceduresDbContext.SaveChangesAsync();
        }

        public async Task Remove(int templateId)
        {
            var template = await _proceduresDbContext.Templates
                .FindAsync(templateId) 
                ?? throw new EntityNotFoundException("Template by id [{procedureId}] not found");
            if (template == null) return;
            _proceduresDbContext.Templates.Remove(template);
            await _proceduresDbContext.SaveChangesAsync();
        }

        public async Task<string> DownloadFileAsync(TemplateDto template, string fileName, string outputPath)
        {
            var filePath = $"{outputPath}/{fileName}{template.FileType}";
            var dataFile = await _proceduresDbContext.Templates
                .Where(f => f.Id == template.Id)
                .Select(f => f.DataFile)
                .FirstAsync();
            using (var fileWriter = File.Create(filePath))
            {
                fileWriter.Write(dataFile);
            }
            return filePath;
        }

        public async Task<List<TemplateDto>> GetAll()
        {
            return await _proceduresDbContext.Templates
                .Select(t => t.ToDto())
                .ToListAsync();
        }

        public async Task<TemplateDto?> GetByProcedureId(int procedureId)
        {
            return await _proceduresDbContext
                .Templates
                .Where(t => t.ProcedureId == procedureId)
                .Select(t => t.ToDto())
                .FirstOrDefaultAsync();
        }

        public void CheckExistTemplate(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotExistException($"File by path [{path}] not exist");
            }
        }
    }
}
