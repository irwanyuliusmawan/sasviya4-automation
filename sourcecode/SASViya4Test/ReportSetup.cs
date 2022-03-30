using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using System.IO;

namespace SASViya4Test
{
    [SetUpFixture]
    public class ReportSetup
    {
        public static ExtentReports _extent;

        [OneTimeSetUp]
        protected void Setup()
        {
            string env = TestContext.Parameters.Get("environment");
            string finalPath;
           
            if (env == "Linux")
            {
                finalPath = TestContext.Parameters.Get("screenshotFilepath");
            }
            else
            {
                var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                var actualPath = path.Substring(0, path.LastIndexOf("bin"));
                var projectPath = new Uri(actualPath).LocalPath;
                finalPath = projectPath.ToString();
                Directory.CreateDirectory(finalPath + "Reports");
                Directory.CreateDirectory(finalPath + "Reports\\" + "Screenshots");
            }

            var reportPath = finalPath + "Reports\\ExtentReport.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            //var configPath = finalPath + "extent-config.xml";
            //htmlReporter.LoadConfig(configPath);

            _extent = new ExtentReports();
            _extent.AttachReporter(htmlReporter);
            _extent.AddSystemInfo("Host Name", TestContext.Parameters.Get("host"));
            _extent.AddSystemInfo("Environment", TestContext.Parameters.Get("buildenv"));
            _extent.AddSystemInfo("UserName", TestContext.Parameters.Get("username"));
            _extent.AddSystemInfo("OS", TestContext.Parameters.Get("environment"));
        }

        [OneTimeTearDown]
        protected void TearDown()
        {
            _extent.Flush();
        }
    }
}
