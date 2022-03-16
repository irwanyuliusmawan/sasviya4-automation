using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using System.Threading;

namespace SASViya4Test
{
    [TestFixture]
    public class SASViyaDriveTest
    {
        IWebDriver driver;

        string env;
        string url;
        string Validationfilepath;
        string folderPath;
        public TestContext TestContext { get; set; }

        [SetUp]
        public void SetUp()
        {
            env = TestContext.Parameters.Get("environment");
            if (env == "Linux")
            {
                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--headless");
                chromeOptions.AddArgument("--no-sandbox");
                chromeOptions.AddArgument("--ignore-certificate-errors");
                driver = new ChromeDriver("/usr/bin", chromeOptions);
            }
            else
            {
                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--ignore-ssl-errors=yes");
                chromeOptions.AddArgument("--ignore-certificate-errors");
                driver = new ChromeDriver(TestContext.Parameters.Get("DriverPath"), chromeOptions);
            }

            url = TestContext.Parameters.Get("SASDriveUrl");
            folderPath = TestContext.Parameters.Get("screenshotFilepath");
            Validationfilepath = folderPath + "/SASDrive/";
        }

        [Test, Order(1), Category("Playlist1")]
        [TestCase(TestName = "Access SAS Drive")]
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

            Thread.Sleep(6000);
            driver.FindElement(By.Id("SASDrive_iframe"));
            driver.SwitchTo().Frame(driver.FindElement(By.Id("SASDrive_iframe")));

            Automation.WaitUntilElementExists(driver, "//div[@id='SASDrive_appContainer']", 30);
            if (Automation.WaitUntilElementExists(driver, "//button[@id='driveHelpOverlayDialog_0-closeXButton']']", 30))
            {
                driver.FindElement(By.XPath("//button[@id='driveHelpOverlayDialog_0-closeXButton']")).Click();
                Thread.Sleep(6000);
            }              
            Automation.GetScreenshot(driver, Validationfilepath + "drives.png", env);
            TestContext.AddTestAttachment(Validationfilepath + "drives.png");
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
