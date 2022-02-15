using Microsoft.Data.SqlClient;
using StoredProcedureExecutor.Configurations;
using StoredProcedureExecutor.Dtos;
using StoredProcedureExecutor.Exceptions;
using StoredProcedureExecutor.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Services.Implementations
{
    public class ProcExecutorService : IProcExecutorService
    {
        private readonly IPlainQueryExecutorService _plainQueryExecutorService;
        private readonly DbConfiguration _dbConfiguration;
        public ProcExecutorService(IPlainQueryExecutorService plainQueryExecutorService, DbConfiguration dbConfiguration)
        {
            _plainQueryExecutorService = plainQueryExecutorService;
            _dbConfiguration = dbConfiguration;
        }

        public async Task CheckExistProcedure(IProcedureInfo procedureInfo)
        {
            var query = "SELECT COUNT(object_id) FROM sys.objects WHERE SCHEMA_NAME(schema_id) = @schema and name = @procedureName";
            var parameters = BuildProcedureSearchParams(procedureInfo);
            var connectionString = BuildConnectionString(procedureInfo);
            Func<DbDataReader, int> map = (result) => result.GetInt32(0);
            var procNotFoundException = new ProcedureNotFoundException($"Procedure with name [{procedureInfo.Name}] on schema [{procedureInfo.Schema}] not found");
            try
            {
                var result = await _plainQueryExecutorService.RawSqlQueryAsync(connectionString, query, parameters, map);
                var existProcedureCount = result.FirstOrDefault();
                if (existProcedureCount == 0)
                {
                    throw procNotFoundException;
                }
            }
            catch (SqlException)
            {
                throw procNotFoundException;
            }

        }

        public async Task ExecuteProc(IProcedureInfo procedureInfo, IEnumerable<IBaseParamInfo>? paramInfos)
        {
            var query = $"{procedureInfo.Schema}.{procedureInfo.Name}";
            List<SqlParameter>? parameters = null;
            if (paramInfos != null)
            {
                parameters = new List<SqlParameter>();
                foreach (var param in paramInfos)
                {
                    parameters.Add(new SqlParameter(param.Name, param.Value));
                }
            }
            var commandDto = new ExecuteCommandDto
            {
                connectionString = BuildConnectionString(procedureInfo),
                Query = query,
                Parameters = parameters,
                RetryCount = _dbConfiguration.ProcExecRetryCount,
                RetryDelay = _dbConfiguration.ProcExecRetryDelay
            };
            await _plainQueryExecutorService.ExecuteCommandAsync(commandDto);
        }

        public List<string>? GetAvailableServers()
        {
            return _dbConfiguration.AvailabeleExecProcOnServers;
        }

        public async Task<IEnumerable<ParamInfo>> GetProcedureParamsInfo(IProcedureInfo procedureInfo)
        {
            var query = "SELECT p.name, TYPE_NAME(P.user_type_id) AS type " +
                            " FROM sys.objects so INNER JOIN sys.parameters AS p ON so.OBJECT_ID = p.OBJECT_ID" +
                            " WHERE SCHEMA_NAME(so.schema_id) = @schema and so.name = @procedureName";
            var parameters = BuildProcedureSearchParams(procedureInfo);
            Func<DbDataReader, ParamInfo> map = (result) => new ParamInfo { Name = result.GetString(0), Type = Enum.Parse<SqlDbType>(result.GetString(1), true) };
            return await _plainQueryExecutorService.RawSqlQueryAsync(BuildConnectionString(procedureInfo), query, parameters, map);
        }

        private string BuildConnectionString(IProcedureInfo procedureInfo)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(_dbConfiguration.ConnectionString);
            connectionStringBuilder.DataSource = procedureInfo.Server;
            connectionStringBuilder.InitialCatalog = procedureInfo.Database;
            connectionStringBuilder.ConnectTimeout = _dbConfiguration.ConnectionTimeout;
            connectionStringBuilder.CommandTimeout = _dbConfiguration.CommandTimeout;
            return connectionStringBuilder.ToString();
        }

        private List<SqlParameter> BuildProcedureSearchParams(IProcedureInfo procedureInfo)
        {
            return new List<SqlParameter>
            {
                new SqlParameter ("@schema",procedureInfo.Schema),
                new SqlParameter ("@procedureName",procedureInfo.Name)
            };
        }
    }
}
