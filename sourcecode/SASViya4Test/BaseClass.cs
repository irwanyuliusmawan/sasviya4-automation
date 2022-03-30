using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace SASViya4Test
{
    public class BaseClass
    {
        public IWebDriver _driver;
        protected ExtentTest _test;

        [SetUp]
        public void initialize()
        {
            string env = TestContext.Parameters.Get("environment");
            if (env == "Linux")
            {
                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--headless");
                chromeOptions.AddArgument("--no-sandbox");
                chromeOptions.AddArgument("--ignore-certificate-errors");
                _driver = new ChromeDriver("/usr/bin", chromeOptions);
            }
            else
            {
                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--ignore-ssl-errors=yes");
                chromeOptions.AddArgument("--ignore-certificate-errors");
                _driver = new ChromeDriver(TestContext.Parameters.Get("DriverPath"), chromeOptions);
            }
           
            _test = ReportSetup._extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    DateTime time = DateTime.Now;
                    String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
                    String screenShotPath = Capture(_driver, fileName);
                    _test.Log(Status.Fail, "Fail");
                    _test.Log(Status.Fail, "Snapshot below: " + _test.AddScreenCaptureFromPath("Screenshots\\" + fileName));
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    _test.Log(Status.Pass, "Pass");
                    if (GetScreenShotPath() == "")
                    {
                        _test.Log(Status.Pass, "Pass");
                    }
                    else
                    {
                        string[] fileEntries = Directory.GetFiles(GetLocalPath() + "/" + GetScreenShotPath());
                        _test.Log(Status.Pass, GetMessage());

                        foreach (string testcasefileName in fileEntries)
                        {
                            string testcaseScreenCapture = "Screenshots" + GetScreenShotPath() + Path.GetFileName(testcasefileName);
                            _test.Log(Status.Pass, "Snapshot below: " + _test.AddScreenCaptureFromPath(testcaseScreenCapture));
                        }
                    }
                                  
                    break;
            }

            _test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
            _driver.Quit();
        }

        public IWebDriver GetDriver()
        {
            return _driver;
        }
        public static string Capture(IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            var pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            var reportPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(reportPath + "Reports\\" + "Screenshots");
            var finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "Reports\\Screenshots\\" + screenShotName;
            var localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
            return reportPath;
        }

        public string GetScreenShotPath()
        {
            string testCaseScreenPath = "";
            if (TestContext.CurrentContext.Test.Name == "Access SAS Drive")
            {
                testCaseScreenPath = "/SASDrive/";
            }
            else if (TestContext.CurrentContext.Test.Name == "Access Theme Designer")
            {
                testCaseScreenPath = "/SASThemeDesigner/";
            }
            else if (TestContext.CurrentContext.Test.Name == "Conversation Designer Related Test Cases")
            {
                testCaseScreenPath = "/SASConversationDesigner/";
            }
            else if(TestContext.CurrentContext.Test.Name == "Access Data Explorer")
            {
                testCaseScreenPath = "/SASDataExplorer/";
            }
            else if (TestContext.CurrentContext.Test.Name == "Run a profile report")
            {
                testCaseScreenPath = "/SASDataProfiling/";
            }
            else if (TestContext.CurrentContext.Test.Name == "Verify QKB is imported and set as default")
            {
                testCaseScreenPath = "/SASDataQuality1/";
            }
            else if (TestContext.CurrentContext.Test.Name == "Add new modified column to a dataset")
            {
                testCaseScreenPath = "/SASDataQuality2/";
            }
            else if (TestContext.CurrentContext.Test.Name == "Add new calculated column to a dataset")
            {
                testCaseScreenPath = "/SASDataStudio/";
            }
            else if (TestContext.CurrentContext.Test.Name == "Login to Environment Manager")
            {
                testCaseScreenPath = "/EnvironmentManager1/";
            }
            else if (TestContext.CurrentContext.Test.Name == "Create a validation folder")
            {
                testCaseScreenPath = "/EnvironmentManager2/";
            }
            else if (TestContext.CurrentContext.Test.Name == "Access Data page")
            {
                testCaseScreenPath = "/EnvironmentManager3/";
            }
            else if (TestContext.CurrentContext.Test.Name == "Access Servers page")
            {
                testCaseScreenPath = "/EnvironmentManager4/";
            }
            else if (TestContext.CurrentContext.Test.Name == "Import Demographics")
            {
                testCaseScreenPath = "/EnvironmentManager5/";
            }
            else if (TestContext.CurrentContext.Test.Name == "Save Code")
            {
                testCaseScreenPath = "/SASJobFlow1/";
            }
            else if (TestContext.CurrentContext.Test.Name == "Schedule Code")
            {
                testCaseScreenPath = "/SASJobFlow2/";
            }
            else if (TestContext.CurrentContext.Test.Name == "Display the relationship of a simple component")
            {
                testCaseScreenPath = "/SASLineage/";
            }
            else if (TestContext.CurrentContext.Test.Name == "Submit a SAS code")
            {
                testCaseScreenPath = "/SASStudio/";
            }
            else if (TestContext.CurrentContext.Test.Name == "Create a Report")
            {
                testCaseScreenPath = "/SASVisualAnalytics1/";
            }
            else if (TestContext.CurrentContext.Test.Name == "Create a map Report")
            {
                testCaseScreenPath = "/SASVisualAnalytics2/";
            }
            else if (TestContext.CurrentContext.Test.Name == "Check Status of Kubernetes Worker Nodes")
            {
                testCaseScreenPath = "";
            }
            else if (TestContext.CurrentContext.Test.Name == "Check Pods are Running Successfully")
            {
                testCaseScreenPath = "";
            }
            
            return testCaseScreenPath;
        }

        public string GetMessage()
        {
            string message = "";
            if (TestContext.CurrentContext.Test.Name == "Access SAS Drive")
            {
                message = "Validated SAS Drive URL - " + TestContext.Parameters.Get("SASDriveUrl");
            }
            else if (TestContext.CurrentContext.Test.Name == "Access Theme Designer")
            {
                message = "Validated Theme Designer URL - " + TestContext.Parameters.Get("SASThemeDesigner");
            }
            else if (TestContext.CurrentContext.Test.Name == "Conversation Designer Related Test Cases")
            {
                message = "Validated Conversation Designer / BOT creation URL - " + TestContext.Parameters.Get("SASConversationDesignerUrl");
            }
            else if (TestContext.CurrentContext.Test.Name == "Access Data Explorer")
            {
                message = "Validated SAS Data Explorer URL - " + TestContext.Parameters.Get("SASDataExplorerUrl");
            }
            else if (TestContext.CurrentContext.Test.Name == "Run a profile report")
            {
                message = "Validated And Executed Profile Report using SAS Data Studio URL - " + TestContext.Parameters.Get("SASDataStudioUrl");
            }
            else if (TestContext.CurrentContext.Test.Name == "Verify QKB is imported and set as default")
            {
                message = "Validated SAS Data Quality feature using SAS Data Studio URL  - " + TestContext.Parameters.Get("SASDataStudioUrl");
            }
            else if (TestContext.CurrentContext.Test.Name == "Add new modified column to a dataset")
            {
                message = "Validated SAS Data Quality feature using SAS Data Studio URL - " + TestContext.Parameters.Get("SASDataStudioUrl");
            }
            else if (TestContext.CurrentContext.Test.Name == "Add new calculated column to a dataset")
            {
                message = "Validated SAS Data Studio URL - " + TestContext.Parameters.Get("SASDriveUrl");
            }
            else if (TestContext.CurrentContext.Test.Name == "Login to Environment Manager")
            {
                message = "Validated Users using Environment Manager URL - " + TestContext.Parameters.Get("SASEnvMgrUrl");
            }
            else if (TestContext.CurrentContext.Test.Name == "Create a validation folder")
            {
                message = "Validated Folder Access using Environment Manager URL - " + TestContext.Parameters.Get("SASEnvMgrUrl");
            }
            else if (TestContext.CurrentContext.Test.Name == "Access Data page")
            {
                message = "Validated CSV Data Access using Environment Manager URL - " + TestContext.Parameters.Get("SASEnvMgrUrl");
            }
            else if (TestContext.CurrentContext.Test.Name == "Access Servers page")
            {
                message = "Validated CAS Servers using Environment Manager URL - " + TestContext.Parameters.Get("SASEnvMgrUrl");
            }
            else if (TestContext.CurrentContext.Test.Name == "Import Demographics")
            {
                message = "Validated Demographic Import using Environment Manager URL - " + TestContext.Parameters.Get("SASEnvMgrUrl");
            }
            else if (TestContext.CurrentContext.Test.Name == "Save Code")
            {
                message = "Validated Save Code for Scheduling using SAS Studio URL - " + TestContext.Parameters.Get("SASStudioUrl");
            }
            else if (TestContext.CurrentContext.Test.Name == "Schedule Code")
            {
                message = "Validated Job Flow and Schdule Code using SAS Studio URL - " + TestContext.Parameters.Get("SASStudioUrl");
            }
            else if (TestContext.CurrentContext.Test.Name == "Display the relationship of a simple component")
            {
                message = "Validated the Lineage Report using SAS Lineage URL - " + TestContext.Parameters.Get("SASLineageUrl");
            }
            else if (TestContext.CurrentContext.Test.Name == "Submit a SAS code")
            {
                message = "Validated by Submiting SAS Code using SAS Studio URL - " + TestContext.Parameters.Get("SASStudioUrl");
            }
            else if (TestContext.CurrentContext.Test.Name == "Create a Report")
            {
                message = "Validated by Creating Report using SAS Visual Analytics URL - " + TestContext.Parameters.Get("SASVisualAnalyticsUrl");
            }
            else if (TestContext.CurrentContext.Test.Name == "Create a map Report")
            {
                message = "Validated by Creating Report using SAS Visual Analytics URL - " + TestContext.Parameters.Get("SASVisualAnalyticsUrl");
            }
            else if (TestContext.CurrentContext.Test.Name == "Check Status of Kubernetes Worker Nodes")
            {
                message = "Validated the Kubernetes Node Status";
            }
            else if (TestContext.CurrentContext.Test.Name == "Check Pods are Running Successfully")
            {
                message = "Validated the Kubernetes Pods Status";
            }

            return message;
        }

        public string GetLocalPath()
        {
            string env = TestContext.Parameters.Get("environment");
            string finalPath;

            if (env == "Linux")
            {
                finalPath = TestContext.Parameters.Get("screenshotFilepath") + "Screenshots/";
            }
            else
            {
                var pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                var actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
                var reportPath = new Uri(actualPath).LocalPath;
                finalPath = pth.Substring(0, pth.LastIndexOf("bin")) + "Reports\\Screenshots\\";
            }
            var localpath = new Uri(finalPath).LocalPath;
            return localpath;
        }
    }
}
