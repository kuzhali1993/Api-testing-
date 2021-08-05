using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFramework_Document360.Utilities
{

    public class ExcelModel
    {
        public String APIName { get; set; }
        public string path { get; set; }
        public String TestScenarios { get; set; }
        public String RequestBoby { get; set; }
        public string schema { get; set; }
        public String ExpectedStatuscode { get; set; }
       // public String Expectedvalues { get; set; }
        
        
    }
    public class TestDataAccess
    {
        private static Dictionary<string, List<ExcelModel>> Sheets = new Dictionary<string, List<ExcelModel>>();

        public static void ReadsheetData()
        {
            List<ExcelModel> datalist = new List<ExcelModel>();
            String file_path = @"D:\Automation\Local Repo(API Testing)\APIFramework_Document360\CA1\TestData Repro\Document360 public apis.xlsx";
            String excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file_path + ";Extended Properties=Excel 12.0;Persist Security Info=False";
            using (OleDbConnection excelConnection = new OleDbConnection(excelConnectionString))
            {
                // Get the sheets name
                List<String> sheetnames = Excel.GetSheetNames(file_path);
                foreach (String sheetname in sheetnames)
                {
                    DataTable dataTableObj = new DataTable("ExcelDataTable");
                    OleDbDataAdapter adp = new OleDbDataAdapter("SELECT * FROM [" + sheetname + "$]", excelConnectionString);
                    adp.Fill(dataTableObj);

                    foreach (DataRow row in dataTableObj.Rows)
                    {
                        ExcelModel temp = new ExcelModel()
                        {
                            TestScenarios = row["Test Scenarios"].ToString(),

                            RequestBoby = BaseClass.ReplaceEnvVariable(row["Request Boby"].ToString()),
                            // ReponseBody = i["Reponse Body"].ToString(),
                            path = row["Path"].ToString(),
                            APIName = row["API Name"].ToString(),
                            schema = row["Response Body schema"].ToString()
                            

                        };
                        datalist.Add(temp);

                    }
                    Sheets.Add(sheetname, datalist);
                }
            }
        }

        public static dynamic GetApiData(string sheetname,string apiname)
        {
            var row = Sheets[sheetname].Where(m => m.APIName.Contains(apiname)).FirstOrDefault();
            return row;
        }

        public static string GetrequestBody(string sheetname,string testscenario)
        {
            DataTable dt = new DataTable();
            String json = Sheets[sheetname].Where(m => m.TestScenarios.Contains(testscenario)).FirstOrDefault().RequestBoby;
                //.Select(f => f.RequestBoby.ToString());
           // var d = Sheets[sheetname].Where()
            return json;
        }
    }
}
