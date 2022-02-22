using Microsoft.EntityFrameworkCore;
using StoredProcedureExecutor.Configurations;
using StoredProcedureExecutor.Constants;
using StoredProcedureExecutor.Enums;
using StoredProcedureExecutor.Models;
using StoredProcedureExecutor.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Data
{
    public class ProceduresDbContext : DbContext, IProceduresDbContext
    {
        private readonly DbConfiguration _configuration;

        public ProceduresDbContext(DbConfiguration dbConfiguration)
        {
            _configuration = dbConfiguration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.ConnectionString,
                    o => o.CommandTimeout(_configuration.CommandTimeout)
                        .MigrationsHistoryTable(BuildTableName("__EFMigrationsHistory"), _configuration.Schema)
                        .EnableRetryOnFailure(_configuration.ConnectRetryCount))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var schema = _configuration.Schema;
            modelBuilder.Entity<Procedure>(b =>
            {
                b.ToTable(BuildTableName("Procedures"), schema);
                b.HasKey(p => p.Id);
                b.Property(p => p.Server).HasMaxLength(ColumnConstraints.ServerMaxLenght);
                b.Property(p => p.Database).HasMaxLength(ColumnConstraints.DatabaseMaxLenght);
                b.Property(p => p.Schema).HasMaxLength(ColumnConstraints.SchemaMaxLenght);
                b.Property(p => p.Name).HasMaxLength(ColumnConstraints.ProcedureMaxLength);
                b.Property(p => p.Description).HasMaxLength(ColumnConstraints.DescriptionMaxLength);
                b.Property(p => p.EmailRecipients).HasMaxLength(ColumnConstraints.EmailRecipientsMaxLength);
                b.Property(p => p.EmailSubject).HasMaxLength(ColumnConstraints.EmailSubjectMaxLenght);
                b.Property(p => p.OutputReportPath).HasMaxLength(ColumnConstraints.OutputReportPath);
                b.Property(p => p.LastUsername).HasMaxLength(ColumnConstraints.LastUsernameMaxLength);
            });

            modelBuilder.Entity<ProcedureParam>(b =>
            {
                b.ToTable(BuildTableName("ProcedureParams"), schema);
                b.HasKey(p => p.Id);
                b.HasOne(p => p.Procedure)
                    .WithMany(p => p.Params)
                    .OnDelete(DeleteBehavior.Cascade);
                b.Property(p => p.Name).HasMaxLength(ColumnConstraints.ParamNameMaxLength);
            });

            modelBuilder.Entity<Template>(b =>
            {
                b.ToTable(BuildTableName("Templates"), schema)
                    .HasKey(t => t.Id);
                b.HasOne(t => t.Procedure)
                    .WithOne(p => p.Template)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ExecStatistic>(b =>
            {
                b.ToTable(BuildTableName("ExecStatistics"), schema)
                    .HasKey(t => t.Id);
                b.Property(s => s.OperationType)
                    .HasConversion(
                        o => o.ToString(),
                        o => (OperationType)Enum.Parse(typeof(OperationType), o));
            });
        }

        public DbSet<Procedure> Procedures => Set<Procedure>();
        public DbSet<ProcedureParam> ProcedureParams => Set<ProcedureParam>();
        public DbSet<Template> Templates => Set<Template>();
        public DbSet<ExecStatistic> Statistics => Set<ExecStatistic>();

        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
            base.ChangeTracker.Clear();
        }

        private string BuildTableName(string name)
        {
            return $"{_configuration.Prefix}_{name}";
        }
    }
}