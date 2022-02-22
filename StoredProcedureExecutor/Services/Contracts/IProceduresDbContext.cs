using Microsoft.EntityFrameworkCore;
using StoredProcedureExecutor.Models;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Services.Contracts
{
    public interface IProceduresDbContext
    {
        public DbSet<Procedure> Procedures { get; }
        public DbSet<ProcedureParam> ProcedureParams { get; }
        public DbSet<Template> Templates { get; }
        public DbSet<ExecStatistic> Statistics { get; }
        Task SaveChangesAsync();
    }
}