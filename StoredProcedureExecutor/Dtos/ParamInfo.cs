using StoredProcedureExecutor.Services.Contracts;
using System.Data;

namespace StoredProcedureExecutor.Dtos
{
    public class ParamInfo : IBaseParamInfo
    {
        public string Name { get; set; } = string.Empty;
        public SqlDbType Type { get; set; }
        public string? Value { get; set; }
    }
}