using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using System.Threading;

namespace SASViya4Test
{
    [TestFixture]
    public class SASViyaDataProfilingTest
    {
        IWebDriver driver;
        string url;
        string Validationfilepath;
        string folderPath;
        public TestContext TestContext { get; set; }

        [SetUp]
        public void SetUp()
        {
            string env = TestContext.Parameters.Get("environment");
            if (env == "Linux")
            {
                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--headless");
                chromeOptions.AddArgument("--no-sandbox");
                driver = new ChromeDriver("/usr/bin", chromeOptions);
            }
            else
            {
                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--ignore-ssl-errors=yes");
                chromeOptions.AddArgument("--ignore-certificate-errors");
                driver = new ChromeDriver(TestContext.Parameters.Get("DriverPath"), chromeOptions);
            }

            url = TestContext.Parameters.Get("SASDataExplorerUrl");
            folderPath = TestContext.Parameters.Get("screenshotFilepath");
            Validationfilepath = folderPath + "/SASDataProfiling/";
        }

        [Test, Order(1)]
        [TestCase(TestName = "Run a profile report")]
        public void Profiling()
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
            driver.FindElement(By.Id("data_explorer_ui_iframe"));
            driver.SwitchTo().Frame(driver.FindElement(By.Id("data_explorer_ui_iframe")));

            Automation.WaitUntilElementExists(driver, "//div[@id='data_explorer_ui']", 60);
            Thread.Sleep(6000);
            
            driver.FindElement(By.XPath("//ul/li[1]")).Click();
            Thread.Sleep(8000);
            driver.FindElement(By.XPath("//div[@id='__xmlview1--dataPanelIconTabBar--header-head']/div[3]")).Click();
            Thread.Sleep(8000);

            if(Automation.WaitUntilElementExists(driver, "//div[@id='sasMZeroStateButtonGroup']", 10))
            {
                driver.FindElement(By.XPath("//div[@class='sasMZeroStateButtonGroup']/button")).Click();
            }
            else
            {
                driver.FindElement(By.XPath("//div[@class = 'sapMSBInner']/button")).Click();
            }

            Thread.Sleep(15000);

            Automation.GetScreenshot(driver, Validationfilepath + "profilereport.png");
            TestContext.AddTestAttachment(Validationfilepath + "profilereport.png");
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
