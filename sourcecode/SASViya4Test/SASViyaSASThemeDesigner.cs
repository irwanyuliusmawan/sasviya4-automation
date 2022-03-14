using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using System.Threading;

namespace SASViya4Test
{
    [TestFixture]
    public class SASViyaThemeDesigner
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

            url = TestContext.Parameters.Get("SASThemeDesigner");
            folderPath = TestContext.Parameters.Get("screenshotFilepath");
            Validationfilepath = folderPath + "/SASThemeDesigner/";
        }

        [Test, Order(1)]
        [TestCase(TestName = "Access Theme Designer")]
        public void Login()
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Size = new System.Drawing.Size(1280, 680);
            string username = TestContext.Parameters.Get("username");
            string password = TestContext.Parameters.Get("password");
            driver.FindElement(By.Id("username")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Id("submitBtn")).Click();
            driver.FindElement(By.Id("sas-admin")).Click();

            Thread.Sleep(3000);
            driver.FindElement(By.Id("ThemeDesignerLogon_iframe"));
            driver.SwitchTo().Frame(driver.FindElement(By.Id("ThemeDesignerLogon_iframe")));

            Automation.WaitUntilElementExists(driver, "//div[@id='ThemeDesignerLogon_appContainer']", 50);
            
            Thread.Sleep(3000);
            Automation.GetScreenshot(driver, Validationfilepath + "login.png");
            TestContext.AddTestAttachment(Validationfilepath + "login.png");
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
