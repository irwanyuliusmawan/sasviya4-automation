using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Diagnostics;
using System.Threading;

namespace SASViya4Test
{
    [TestFixture]
    public class SASViyaDataQualityTest
    {
        IWebDriver driver;

        string url;
        string sasStudioUrl;
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

            url = TestContext.Parameters.Get("SASEnvMgrUrl");
            sasStudioUrl = TestContext.Parameters.Get("SASDataStudioUrl");
            folderPath = TestContext.Parameters.Get("screenshotFilepath");
            Validationfilepath = folderPath + "/SASDataQuality/";
        }

        [Test, Order(1)]
        [TestCase(TestName = "Verify QKB is imported and set as default")]
        public void QKBCheck()
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
            Thread.Sleep(8000);

            driver.FindElement(By.XPath("//button[@id='envmgrapp_appContainer_lfn_down']")).Click();
            driver.FindElement(By.XPath("//button[@id='envmgrapp_appContainer_lfn_down']")).Click();
            driver.FindElement(By.XPath("//button[@id='envmgrapp_appContainer_lfn_down']")).Click();
            driver.FindElement(By.XPath("//button[@id='envmgrapp_appContainer_lfn_down']")).Click();
            driver.FindElement(By.XPath("//button[@id='envmgrapp_appContainer_lfn_down']")).Click();
            driver.FindElement(By.XPath("//button[@id='envmgrapp_appContainer_lfn_down']")).Click();
            driver.FindElement(By.XPath("//button[@id='envmgrapp_appContainer_lfn_down']")).Click();
            driver.FindElement(By.XPath("//button[@id='envmgrapp_appContainer_lfn_down']")).Click();
            driver.FindElement(By.XPath("//button[@id='envmgrapp_appContainer_lfn_down']")).Click();
            driver.FindElement(By.XPath("//button[@id='envmgrapp_appContainer_lfn_down']")).Click();
            driver.FindElement(By.XPath("//button[@id='envmgrapp_appContainer_lfn_down']")).Click();
            Thread.Sleep(6000);
            driver.FindElement(By.Id("envmgrapp_appContainer_lfn_23_icn")).Click();
            Thread.Sleep(6000);
            
            Automation.GetScreenshot(driver, Validationfilepath + "QKB.png");
            TestContext.AddTestAttachment(Validationfilepath + "QKB.png");
        }

        [Test, Order(1)]
        [TestCase(TestName = "Add new modified column to a dataset")]
        public void Transform()
        {
            driver.Navigate().GoToUrl(sasStudioUrl);
            driver.Manage().Window.Size = new System.Drawing.Size(1280, 680);
            string username = TestContext.Parameters.Get("username");
            string password = TestContext.Parameters.Get("password");
            driver.FindElement(By.Id("username")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Id("submitBtn")).Click();
            driver.FindElement(By.Id("sas-admin")).Click();

            Thread.Sleep(6000);
            driver.FindElement(By.Id("data_wrangler_ui_iframe"));
            driver.SwitchTo().Frame(driver.FindElement(By.Id("data_wrangler_ui_iframe")));

            Thread.Sleep(6000);
            Automation.WaitUntilElementExists(driver, "//div[@id='data_wrangler_ui']", 80);
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//div[@class='sasMZeroStateButtonGroup']/button[1]")).Click();
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//div[@role='tabpanel']/div/div[2]/div/ul/li[1]")).Click();
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//button[@data-landmark-label='Choose Data OK button']")).Click();
            Thread.Sleep(8000);

            driver.FindElement(By.XPath("//div[@id='contentView--transformContainer--transformShadesContainer']/section[3]/div[2]/div/div/ul/li[1]")).Click();
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//div[@id='contentView--transformContainer']/div/div/button")).Click();
            Thread.Sleep(15000);

            driver.FindElement(By.XPath("//div[@role='combobox' and @title='Lower']")).Click();
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.Enter).Perform();
            new Actions(driver).SendKeys(Keys.Tab).Perform();
            new Actions(driver).SendKeys(Keys.Tab).Perform();
            new Actions(driver).SendKeys(Keys.Tab).Perform();
            new Actions(driver).SendKeys(Keys.Enter).Perform();
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//div[@class='sapMSBInner']/button")).Click();
            Thread.Sleep(15000);

            Automation.GetScreenshot(driver, Validationfilepath + "dataset.png");
            TestContext.AddTestAttachment(Validationfilepath + "dataset.png");
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
