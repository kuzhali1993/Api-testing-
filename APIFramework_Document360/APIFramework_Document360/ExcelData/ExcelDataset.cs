using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIFramework_Document360.Utilities;

namespace APIFramework_Document360.ExcelData
{
    public class ExcelModel
    {
        public String  APIName { get; set; }
        public String TestScenarios { get; set; }
        public String RequestBoby { get; set; }
        public String ReponseBody { get; set; }
        public String ExpectedStatuscode { get; set; }
        public String Expectedvalues { get; set; }
        public string path { get; set; }
        public string schema { get; set; }
    }

    public enum sheet
    {
        kuzhali
        
    }
    public class ExcelDataset
    {
        
        public static Dictionary<String, List<ExcelModel>> Sheets = new Dictionary<string, List<ExcelModel>>();
        public static List<ExcelModel> Category { get; set; }
        public static List<ExcelModel> ProjectVersion { get; set; }

       // public static List<ExcelModel> 
       

    }
    
    public class ExcelActions
    {

        public static void dynamicSheets()
        {
            List<ExcelModel> datalist = new List<ExcelModel>();
            String file_path = @"D:\Automation\Local Repo(API Testing)\APIFramework_Document360\CA1\TestData Repro\Document360 public apis.xlsx";
            String excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file_path + ";Extended Properties=Excel 12.0;Persist Security Info=False";
            using (OleDbConnection excelConnection = new OleDbConnection(excelConnectionString))
            {
                // Get the sheets name
                List<String> sheetnames=Excel.GetSheetNames(file_path);
                foreach(String sheetname in sheetnames)
                {
                    DataTable dataTableObj = new DataTable("ExcelDataTable");
                    OleDbDataAdapter adp = new OleDbDataAdapter("SELECT * FROM [" + sheetname + "$]", excelConnectionString);
                    adp.Fill(dataTableObj);
                    
                    foreach(DataRow row in dataTableObj.Rows)
                    {
                        ExcelModel temp = new ExcelModel()
                        {
                            TestScenarios = row["Test Scenarios"].ToString(),
                            RequestBoby = row["Request Boby"].ToString(),
                            // ReponseBody = i["Reponse Body"].ToString(),
                            path = row["Path"].ToString(),
                            APIName = row["API Name"].ToString(),
                            schema = row["Response Body schema"].ToString()


                        };
                        datalist.Add(temp);

                    }
                    ExcelDataset.Sheets.Add(sheetname,datalist);
                }

                
               
            }
        }
        public static List<ExcelModel> readExcel(string str)
        {
            String file_path = @"D:\Automation\Local Repo(API Testing)\APIFramework_Document360\CA1\TestData Repro\Document360 public apis.xlsx";
            String excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file_path + ";Extended Properties=Excel 12.0;Persist Security Info=False";
            List<ExcelModel> data = new List<ExcelModel>();
            using (OleDbConnection excelConnection = new OleDbConnection(excelConnectionString))
            {


                DataTable dataTableObj = new DataTable("ExcelDataTable");
                OleDbDataAdapter adp = new OleDbDataAdapter("SELECT * FROM [" + str + "$]", excelConnectionString);
                adp.Fill(dataTableObj);
                if(dataTableObj.Columns.Count<=0)
                {
                    Console.WriteLine("datatable is empty");
                }
                else 
                {
                    //foreach(DataColumn i in dataTableObj.Columns)
                    //{
                    //    Console.WriteLine(i.ColumnName);
                    //}
                    foreach (DataRow i in dataTableObj.Rows)
                    {
                        ExcelModel temp = new ExcelModel()
                        {
                            TestScenarios = i["Test Scenarios"].ToString(),
                            RequestBoby = i["Request Boby"].ToString(),
                            // ReponseBody = i["Reponse Body"].ToString(),
                            path = i["Path"].ToString(),
                            APIName = i["API Name"].ToString(),
                            schema = i["Response Body schema"].ToString()

                            
                        };
                      //  Console.WriteLine(temp.APIName);
                        data.Add(temp);
                    }
                }
            }
            return data;
        }

    }
}
