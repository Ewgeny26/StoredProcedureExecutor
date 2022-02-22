using StoredProcedureExecutor.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace StoredProcedureExecutor.Services.Implementations
{
    public class ExcelTemplateProviderService : ITemplateProviderService
    {
        public async Task RefreshDataAsync(string pathToTemplate, IEnumerable<IBaseParamInfo>? paramInfoList)
        {
            await Task.Run(() => { RefreshData(pathToTemplate, paramInfoList); });
        }

        private static void RefreshData(string pathToTemplate, IEnumerable<IBaseParamInfo>? paramInfoList)
        {
            Excel.Application? excelApp = null;
            Excel.Workbook? workbook = null;
            try
            {
                excelApp = new Excel.Application
                {
                    Visible = false,
                    DisplayAlerts = false
                };
                workbook = excelApp.Workbooks.Open(pathToTemplate);
                for (var i = 1; i <= workbook.Queries.Count; i++)
                {
                    workbook.Queries[i].Formula = AddQueriesParams(workbook.Queries[i].Formula, paramInfoList);
                }

                workbook.RefreshAll();
                excelApp.Application.CalculateUntilAsyncQueriesDone();
                workbook.Close(true);
                excelApp.Quit();
            }
            finally
            {
                if (workbook != null)
                {
                    Marshal.ReleaseComObject(workbook);
                }

                if (excelApp != null)
                {
                    Marshal.ReleaseComObject(excelApp);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                KillNotDisposingExcelProcess();
            }
        }

        private static void KillNotDisposingExcelProcess()
        {
            var excelProcesses = Process.GetProcessesByName("excel");
            foreach (var process in excelProcesses)
            {
                if (string.IsNullOrWhiteSpace(process.MainWindowTitle))
                {
                    process.Kill();
                }
            }
        }

        private static string AddQueriesParams(string excelQuery, IEnumerable<IBaseParamInfo>? paramInfoList)
        {
            var queryBuilder = new StringBuilder(excelQuery);
            if (paramInfoList == null)
            {
                return queryBuilder.ToString();
            }

            foreach (var paramInfo in paramInfoList)
            {
                queryBuilder.Replace(paramInfo.Name, paramInfo.Value);
            }

            return queryBuilder.ToString();
        }
    }
}