using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using System.Threading;

namespace SASViya4Test
{
    public class SASViyaDataExplorerTest : BaseClass
    {
        IWebDriver driver;

        string env;
        string url;
        string Validationfilepath;
        public TestContext TestContext { get; set; }

        [SetUp]
        public void SetUp()
        {
            env = TestContext.Parameters.Get("environment");
            url = TestContext.Parameters.Get("SASEnvMgrUrl");
            driver = GetDriver();
            Validationfilepath = GetLocalPath() + GetScreenShotPath();
        }

        [Test, Order(1), Category("Playlist2")]
        [TestCase(TestName = "Access Data Explorer")]
        public void DataExplorer()
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Size = new System.Drawing.Size(1280, 680);
            string username = TestContext.Parameters.Get("username");
            string password = TestContext.Parameters.Get("password");
            driver.FindElement(By.Id("username")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Id("submitBtn")).Click();
            driver.FindElement(By.Id("sas-admin")).Click();

            Thread.Sleep(6000);
            driver.FindElement(By.Id("envmgrapp_iframe"));
            driver.SwitchTo().Frame(driver.FindElement(By.Id("envmgrapp_iframe")));

            Automation.WaitUntilElementExists(driver, "//div[@id='envmgrapp']", 60);
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//button[@id='envmgrapp_appContainer_banner-appSwitcher']")).Click();
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//ul/li[2]")).Click();
            Automation.WaitUntilElementExists(driver, "//div[@id='data_explorer_ui']", 60);
            Thread.Sleep(6000);

            Automation.GetScreenshot(driver, Validationfilepath + "data.png", env);
            TestContext.AddTestAttachment(Validationfilepath + "data.png");
        }

        [TearDown]
        public void closeBrowser()
        {
            try
            {
                driver.Close();
                driver.Quit();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
