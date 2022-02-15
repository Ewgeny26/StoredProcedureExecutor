using StoredProcedureExecutor.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Services.Contracts
{
    public interface IPlainQueryExecutorService
    {
        Task<List<T>> RawSqlQueryAsync<T>(string connStr, string query, IEnumerable<DbParameter>? parameters, Func<DbDataReader, T> map);

        Task ExecuteCommandAsync(ExecuteCommandDto commandDto);
    }
}
