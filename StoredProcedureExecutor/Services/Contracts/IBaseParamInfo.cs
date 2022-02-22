using System.Data;

namespace StoredProcedureExecutor.Services.Contracts
{
    public interface IBaseParamInfo
    {
        public string Name { get; set; }
        public SqlDbType Type { get; set; }
        public string? Value { get; set; }
    }
}