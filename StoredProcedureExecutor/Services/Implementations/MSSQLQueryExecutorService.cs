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
    public class MssqlQueryExecutorService : IPlainQueryExecutorService
    {
        public async Task<List<T>> RawSqlQueryAsync<T>(string connStr, string query,
            IEnumerable<DbParameter>? parameters, Func<DbDataReader, T> map)
        {
            await using (var connection = new SqlConnection(connStr))
            {
                await using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    await connection.OpenAsync();

                    await using (var result = await command.ExecuteReaderAsync())
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
            await using (var connection = new SqlConnection(commandDto.ConnectionString))
            {
                await using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandDto.Query;
                    command.CommandType = CommandType.StoredProcedure;
                    if (commandDto.Parameters != null)
                    {
                        command.Parameters.AddRange(commandDto.Parameters.ToArray());
                    }

                    await connection.OpenAsync();
                    var retryExecutor = new RetryExecutor(commandDto.RetryCount, commandDto.RetryDelay);
                    await retryExecutor.RetryAsync(() => Exec(connection, command));
                }
            }
        }

        private static async Task Exec(DbConnection connection, DbCommand sqlCommand)
        {
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            await sqlCommand.ExecuteNonQueryAsync();
        }
    }
}