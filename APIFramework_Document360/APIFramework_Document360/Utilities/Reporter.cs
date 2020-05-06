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
    class Reporter : BaseClass
    {
        public static ExtentReports report;
        public static ExtentTest parentLogger;
        public static ExtentTest testcaseLogger;
        public static ExtentHtmlReporter HtmlReport;
        
       
        public static void CreateReport()
        {
            string path = BaseClass.GetFolderPath("Reports");
            //String ReportPath = path + "MyReport.html";

            HtmlReport = new ExtentHtmlReporter(path);
            HtmlReport.LoadConfig("D:\\Back_up_important\\Local Repo(API Testing)\\APIFramework_Document360\\APIFramework_Document360\\extent-config.xml");
            HtmlReport.Config.DocumentTitle = "Document360";
            HtmlReport.Config.ReportName = "Edited reportername";
            
            try
            {
                report = new ExtentReports();
                report.AddSystemInfo("HostName", "Kuzhali");
                report.AddSystemInfo("Environment", "QA");
                report.AddSystemInfo("Reporter Name", "Kuzhali");

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

                testcaseLogger = parentLogger.CreateNode(TestContext.CurrentContext.Test.Name);
                string path = GetFolderPath(TestContext.CurrentContext.Test.ClassName);
                
                //Logger = report.CreateTest(TestContext.CurrentContext.Test.Name);
               // testcaseLogger.AssignCategory(lastFolderName);
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
               
                testcaseLogger.Log(Status.Fail,MarkupHelper.CreateLabel("sample text",ExtentColor.Red));
              //  testcaseLogger.Pass()
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
                var stacktrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";

                var error = TestContext.CurrentContext.Result.Message;
               
                switch (status)
                {
                    case TestStatus.Failed:
                        testcaseLogger.Log(Status.Fail, "Testcase Failed with error: -" + error);
                        break;
                    case TestStatus.Skipped:
                        testcaseLogger.Log(Status.Skip, "Testcase skipped!");
                        break;
                    default:
                        testcaseLogger.Log(Status.Pass,"Testcase Passed!");
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
