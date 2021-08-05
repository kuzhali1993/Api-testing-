using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using excel = Microsoft.Office.Interop.Excel;
namespace APIFramework_Document360.Utilities
{
    public class Excel :BaseClass
    {
        private static excel.Application xlApp = null;
        private static excel.Workbooks workbooks = null;
        private static excel.Workbook workbook = null;
        private static excel.Range range = null;
        private static excel.Worksheet worksheet = null;
        private static Hashtable sheets;
        private static string Filepath;
        private static string sheetname;
        private static int Rowcnt;
        private  static int Columncnt;
        private static Dictionary<string, dynamic> variables;

        private enum ColumnIndex
        {
            SNo = 1,
            Api_Name = 2,
            Test_Scenarios = 3,
            Request_Body = 4,
            Response_Body = 5,
            Expected_statuscode = 6,
            Expected_value = 7

        }

        private static void GetExcelInstance()
        {
            
            Filepath = GetFolderPath("TestData Repository") + GetEnvironmentVariable("TestDataExcelFileName");
            xlApp = GetInstance();
            workbooks = xlApp.Workbooks;
            workbook = workbooks.Open(Filepath);
        }

        public static void OpenExcelSheet(string SheetName)
        {
            Filepath = GetFolderPath("TestData Repository") + GetEnvironmentVariable("TestDataExcelFileName");
            xlApp = GetInstance();
            workbooks = xlApp.Workbooks;
            workbook = workbooks.Open(Filepath);
            sheetname = SheetName;
           
            sheets = new Hashtable();
            range = GetUsedRange();
            Rowcnt = range.Rows.Count;
            Columncnt = range.Columns.Count;
        }

        private static excel.Application GetInstance()
        {
            excel.Application instance = null;
            try
            {
                instance = (excel.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                instance = new excel.Application();
            }

            return instance;
        }

       private static excel.Range GetUsedRange()
        {
            if (range == null)
            {
                
                sheets = new Hashtable();
                int sheetindex = GetSheetIndex();
                worksheet = workbook.Worksheets[sheetindex] as excel.Worksheet;
                range = worksheet.UsedRange;
                return range;
            }
            else
            {
                return range;
            }
        }
        private static int GetSheetIndex()
        {

            int count = 1;

            // Storing worksheet names in Hashtable.
            foreach (excel.Worksheet sheet in workbook.Sheets)
            {
                sheets[count] = sheet.Name;
                count++;
            }
            int sheetValue = 0;
            if (sheets.ContainsValue(sheetname))
            {
                foreach (DictionaryEntry sheet in sheets)
                {
                    string str = sheet.Value.ToString().ToLower();
                    
                    if (str.Equals(sheetname.ToLower()))
                    {
                        sheetValue = (int)sheet.Key;
                    }
                }
               
                //worksheet = workbook.Worksheets[sheetValue] as excel.Worksheet;
                //range = worksheet.UsedRange;
                //Rowcnt = range.Rows.Count;
                //Columncnt = range.Columns.Count;
            }
            return sheetValue;
        }

        public static void CloseExcel()
        {
            Marshal.FinalReleaseComObject(worksheet);
            worksheet = null;
            range = null;
            workbook.Close(false, Filepath, null); // Close the connection to workbook
            Marshal.FinalReleaseComObject(workbook); // Release unmanaged object references.
            workbook = null;

            workbooks.Close();
            Marshal.FinalReleaseComObject(workbooks);
            workbooks = null;

            xlApp.Quit();
            Marshal.FinalReleaseComObject(xlApp);
            xlApp = null;
           
        }

        private static string GetCellData(string colName, int rowNumber)
        {
            string value = string.Empty;
            int colNumber = 0;
            //string cellvalue; 


            //GetSheetIndex();



            for (int i = 1; i <= Columncnt; i++)
            {
                //  excel.Range objrange = worksheet.Cells[i, j];

                string colNameValue = Convert.ToString((range.Cells[1, i] as excel.Range).Value2);

                if (colNameValue.ToLower() == colName.ToLower())
                {
                    colNumber = i;
                    break;
                }
            }

            //  Console.WriteLine(cnt);
            value = Convert.ToString((range.Cells[rowNumber, colNumber] as excel.Range).Value2);
            Marshal.FinalReleaseComObject(worksheet);
            worksheet = null;
            CloseExcel();
            return value;
        }

        public static string GetRequestBoby(string Testscenario)
        {
            
            string value = string.Empty;
            int rowNumber = 0;
            int colNumber = (int)ColumnIndex.Test_Scenarios;
            int ReqBody_ColNumber = (int)ColumnIndex.Request_Body;
            string key, payload;
            try
            {
                for (int i = 1; i <= Rowcnt; i++)
                {
                    string TestScenarios = Convert.ToString((range.Cells[i, colNumber] as excel.Range).Value2);
                    if (TestScenarios.ToLower() == Testscenario.ToLower())
                    {
                        rowNumber = i;
                        break;
                    }

                }
                value = Convert.ToString((range.Cells[rowNumber, ReqBody_ColNumber] as excel.Range).Value2);

                //Replace variables in Json with environment variable

                variables = GetcurrentVariables();
                foreach (var items in variables)
                {
                    key = "{{" + items.Key + "}}";
                    if (value.Contains(key))
                    {
                        
                        value = value.Replace(key, items.Value);
                    }
                    
                }
                variables = null;
                return value;
            }
            catch(Exception e)
            {
                return e.Message;
            }

        }

        public static List<string> GetSheetNames(string fpath)
        {
            xlApp = GetInstance();
            workbooks = xlApp.Workbooks;
            workbook = workbooks.Open(fpath);
           // int count = 0;
            List<string> Sheets = new List<string>();
            // Storing worksheet names in Hashtable.
            foreach (excel.Worksheet sheet in workbook.Sheets)
            {
                Sheets.Add(sheet.Name);
            }
            // CloseExcel();
            workbook.Close(false);
            workbooks.Close();
            xlApp.Quit();
            Marshal.FinalReleaseComObject(xlApp);

            return Sheets;
        }
    }
}
