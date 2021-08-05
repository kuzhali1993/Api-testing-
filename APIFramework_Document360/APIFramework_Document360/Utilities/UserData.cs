using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFramework_Document360.Utilities
{
    public class dataModel
    {
       // public String  APIName { get; set; }
        public String TestScenarios { get; set; }
        public String RequestBoby { get; set; }
        public String ReponseBody { get; set; }
        public String ExpectedStatuscode { get; set; }
        public String Expectedvalues  { get; set; }
    }
    public class UserData
    {
        public static List<dataModel> readExcel(string str)
        {
            String file_path = @"D:\Automation\Local Repo(API Testing)\APIFramework_Document360\APIFramework_Document360\TestData Repository\ApiTestData.xlsx";
            String excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file_path + ";Extended Properties=Excel 12.0;Persist Security Info=False";
            List<dataModel> data = new List<dataModel>();
            using (OleDbConnection excelConnection = new OleDbConnection(excelConnectionString))
            {


                DataTable dataTableObj = new DataTable("ExcelDataTable");
                OleDbDataAdapter adp = new OleDbDataAdapter("SELECT * FROM ["+ str + "$]", excelConnectionString);
                adp.Fill(dataTableObj);

               
                foreach (DataRow i in dataTableObj.Rows)
                {
                    dataModel temp = new dataModel()
                    {
                        TestScenarios = i["Test Scenarios"].ToString(),
                        RequestBoby = i["Request Boby"].ToString(),
                        ReponseBody = i["Reponse Body"].ToString()


                    };
                    data.Add(temp);

                }
            }
            return data;
        }


       
      

    }
}
