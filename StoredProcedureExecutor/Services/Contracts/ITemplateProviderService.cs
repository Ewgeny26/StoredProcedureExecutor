using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoredProcedureExecutor.Services.Contracts
{
    public interface ITemplateProviderService
    {
        Task RefreshDataAsync(string pathToTemplate, IEnumerable<IBaseParamInfo>? paramInfoList);
    }
}