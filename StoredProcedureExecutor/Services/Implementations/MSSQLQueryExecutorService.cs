using Microsoft.Data.SqlClient;
using StoredProcedureExecutor.Common;
using StoredProcedureExecutor.Dtos;
using StoredProcedureExecutor.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Services.Implementations
{
    public class MSSQLQueryExecutorService : IPlainQueryExecutorService
    {
        public async Task<List<T>> RawSqlQueryAsync<T>(string connStr, string query, IEnumerable<DbParameter>? parameters, Func<DbDataReader, T> map)
        {

            using (var connection = new SqlConnection(connStr))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());

                    }
                    await connection.OpenAsync();

                    using (var result = await command.ExecuteReaderAsync())
                    {
                        var entities = new List<T>();

                        while (result.Read())
                        {
                            entities.Add(map(result));
                        }

                        return entities;
                    }
                }
            }
        }

        public async Task ExecuteCommandAsync(ExecuteCommandDto commandDto)
        {
            using (var connection = new SqlConnection(commandDto.connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandDto.Query;
                    command.CommandType = CommandType.StoredProcedure;
                    if (commandDto.Parameters != null)
                    {
                        command.Parameters.AddRange(commandDto.Parameters.ToArray());

                    }
                    await connection.OpenAsync();
                    var execFunc = () => Exec(connection, command);
                    var retryExecutor = new RetryExecutor(commandDto.RetryCount, commandDto.RetryDelay);
                    await retryExecutor.RetryAsync(execFunc);
                }
            }
        }

        private async Task Exec(SqlConnection connection, SqlCommand sqlCommand)
        {
            if(connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }
            await sqlCommand.ExecuteNonQueryAsync();
        }


    }
}
