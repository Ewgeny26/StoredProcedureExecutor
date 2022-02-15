using StoredProcedureExecutor.Services.Contracts;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace StoredProcedureExecutor.Services.Implementations
{
    public class ExcelTemplateProviderService : ITemplateProviderService
    {
        public async Task RefreshDataAsync(string pathToTemplate)
        {
            await Task.Run(() => { RefreshData(pathToTemplate); });
        }

        private void RefreshData(string pathToTemplate)
        {
            Excel.Application? excelApp = null;
            Excel.Workbook? workbook = null;
            try
            {
                excelApp = new Excel.Application();
                var name = excelApp.Name;
                excelApp.Visible = false;
                workbook = excelApp.Workbooks.Open(pathToTemplate);
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
                    workbook = null;
                }
                if (excelApp != null)
                {
                    Marshal.ReleaseComObject(excelApp);
                    excelApp = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                KillNotDisposingExcelProcess();
            }
        }

        private void KillNotDisposingExcelProcess()
        {
            Process[] excelProcesses = Process.GetProcessesByName("excel");
            foreach (var process in excelProcesses)
            {
                if (string.IsNullOrWhiteSpace(process.MainWindowTitle))
                {
                    process.Kill();
                }
            }
        }
    }
}
