using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Configuration;
using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Diagnostics;
using AventStack.ExtentReports.MarkupUtils;
using System.IO;

namespace APIFramework_Document360.Utilities
{
    public class Reporter : BaseClass
    {
        public static ExtentReports report;
        public static ExtentTest parentLogger;
        public static ExtentTest testcaseLogger;
        public static ExtentHtmlReporter HtmlReport;
        public static dynamic status;
       
        public static void CreateReport()
        {
            string path = BaseClass.GetFolderPath("Reports");
            //String ReportPath = path + "MyReport.html";

            HtmlReport = new ExtentHtmlReporter(path);
            HtmlReport.LoadConfig(@"D:\Automation\Local Repo(API Testing)\APIFramework_Document360\APIFramework_Document360\extent-config.xml");
            HtmlReport.Config.DocumentTitle = "Document360";
            HtmlReport.Config.ReportName = "Edited reportername";
            
            try
            {
                report = new ExtentReports();
                report.AddSystemInfo("HostName", "Kuzhali");
                report.AddSystemInfo("Environment", "QA");
               // report.AddSystemInfo("Reporter Name", "Kuzhali");

                // HtmlReport.Configuration().DocumentTitle = "Automation Test report";
                //HtmlReport.Configuration().ReportName = "kuzhali";
                report.AttachReporter(HtmlReport);
                
               
            }
            catch (Exception e)

            {
                Console.WriteLine(e.Message);
            }
        }

        public static ExtentReports getInstance()
        {
            if(report == null)
            {
                CreateReport();
            }
            return report;
        }
        public static void CreateLogforApi(string name)
        {
            try
            {
                
                parentLogger = report.CreateTest(name);
                var str = Path.GetFullPath(TestContext.CurrentContext.Test.ClassName);

                string lastFolderName = Path.GetFileName(str);
                var trimstr = lastFolderName.Substring(0, lastFolderName.LastIndexOf('.'));
                var CategoryName = trimstr.Split('.').Last();

                parentLogger.AssignCategory(CategoryName);

            }
            catch (Exception e)
            {
                Console.WriteLine("unable to create Api log "+e.Message);
            }

        }

       
        public static void CreateLogforTest()
        {
            try
            {

                // testcaseLogger = parentLogger.CreateNode(TestContext.CurrentContext.Test.Name);
                testcaseLogger = parentLogger.CreateNode(TestContext.CurrentContext.Test.Properties["test scenarios"].ElementAt(0).ToString());


            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to create testcase Log:" + e.Message);
            }
        }

       public static void LogDetails(string str)
        {
            try
            {
                testcaseLogger.Info(str);
                         
              
            }
            catch (Exception e)
            {
                Console.WriteLine("unable to log info in test:"+e.Message);
            }
        }


        public static void LogResult()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var overallstatus = status;
                var stacktrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";

                var error = TestContext.CurrentContext.Result.Message;
               
                switch (status)
                {
                    case TestStatus.Failed:
                        {
                            testcaseLogger.Log(Status.Fail, MarkupHelper.CreateLabel("Overall staus: Testcase Failed!", ExtentColor.Red, ExtentColor.White));
                            testcaseLogger.Log(Status.Info, MarkupHelper.CreateCodeBlock(error));
                            break;
                        }
                    case TestStatus.Skipped:
                        testcaseLogger.Log(Status.Skip,MarkupHelper.CreateLabel("Overall staus - Testcase skipped!",ExtentColor.Blue,ExtentColor.White));
                        break;
                    default:
                        testcaseLogger.Log(Status.Pass,MarkupHelper.CreateLabel("Overall status - Testcase Passed!", ExtentColor.Green, ExtentColor.White));
                        break;


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            

        }

       
        public static void Flushreport()
        {
            try
            {
                report.Flush();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public enum TestcaseStatus
        {
            Pass,
            Fail,
            Skipped
        }

    }
}
