using StoredProcedureExecutor.Infrastructure;
using System;
using StoredProcedureExecutor.Infrastructure.Implementations;

namespace StoredProcedureExecutor.Dtos
{
    public class TemplateDto : NotifyPropertyChangedBase
    {
        private string _templatePath = string.Empty;

        public string TemplatePath
        {
            get => _templatePath;
            set => SetProperty(ref _templatePath, value);
        }

        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public int ProcedureId { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}