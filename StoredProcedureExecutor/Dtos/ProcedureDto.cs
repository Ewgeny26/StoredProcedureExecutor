using StoredProcedureExecutor.Infrastructure;
using StoredProcedureExecutor.Services.Contracts;
using System;

namespace StoredProcedureExecutor.Dtos
{
    public class ProcedureDto : NotifyPropertyChangedBase, IProcedureInfo
    {
        private string _name = string.Empty;
        private string _server = string.Empty;
        private string _database = string.Empty;
        private string _schema = string.Empty;
        private string? _description;
        private string? _emailRecipients;
        private string? _emailSubject;
        private string? _outputReportPath;
        private DateTime? _lastExecutedAt;
        private DateTime? _lastRefreshedAt;
        private DateTime? _lastSentTemplateAt;
        private string? _lastUsername;

        public string Server
        {
            get => _server;
            set => SetProperty(ref _server, value);
        }
        public string Database
        {
            get => _database;
            set => SetProperty(ref _database, value);
        }
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string Schema
        {
            get => _schema;
            set => SetProperty(ref _schema, value);
        }

        public string? Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        public string? EmailRecipients
        {
            get => _emailRecipients;
            set => SetProperty(ref _emailRecipients, value);
        }
        public string? EmailSubject
        {
            get => _emailSubject;
            set => SetProperty(ref _emailSubject, value);
        }
        public string? OutputReportPath
        {
            get => _outputReportPath;
            set => SetProperty(ref _outputReportPath, value);
        }
        public DateTime? LastExecutedAt
        {
            get => _lastExecutedAt;
            set => SetProperty(ref _lastExecutedAt, value);
        }
        public DateTime? LastRefreshedAt
        {
            get => _lastRefreshedAt;
            set => SetProperty(ref _lastRefreshedAt, value);
        }
        public DateTime? LastSentTemplateAt
        {
            get => _lastSentTemplateAt;
            set => SetProperty(ref _lastSentTemplateAt, value);
        }
        public string? LastUsername
        {
            get => _lastUsername;
            set => SetProperty(ref _lastUsername, value);
        }
        public int? Id { get; init; }

        public override string ToString()
        {
            return $"{Schema}.{Name}";
        }
    }
}
