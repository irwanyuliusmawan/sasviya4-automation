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
    public class SASViyaVisualAnalyticsTest
    {
        IWebDriver driver;
        string url;
        string Reportfilepath;
        string folderPath;
        string demographicfilepath;
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

            url = TestContext.Parameters.Get("SASVisualAnalyticsUrl");
            folderPath = TestContext.Parameters.Get("screenshotFilepath");
            Reportfilepath = folderPath + "/SASVisualAnalytics/";
            demographicfilepath = TestContext.Parameters.Get("demographicfilepath");
        }

        [Test, Order(1)]
        [TestCase(TestName = "Create a Report")]
        public void ReportValiation()
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
            driver.FindElement(By.Id("VANextLogon_iframe"));
            driver.SwitchTo().Frame(driver.FindElement(By.Id("VANextLogon_iframe")));

            Automation.WaitUntilElementExists(driver, "//div[@id='VANextLogon']", 60);
            if (Automation.WaitUntilElementExists(driver, "//button[@id='driveHelpOverlayDialog_0-closeXButton']']", 30))
            {
                driver.FindElement(By.XPath("//button[@id='driveHelpOverlayDialog_0-closeXButton']")).Click();
                Thread.Sleep(3000);
            }
            Thread.Sleep(6000);

            new Actions(driver).SendKeys(Keys.Escape).Perform();
            Thread.Sleep(8000);
            driver.FindElement(By.XPath("//button[@title='Data']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//ul[@role='listbox']/li[1]/div[@class='sapMLIBContent']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//button/span/span/bdi[text()='OK']")).Click();
            Thread.Sleep(3000);
            new Actions(driver).SendKeys(Keys.Escape).Perform();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//button[@title='Data']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//button[@title='Data']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div[@class='vanSidePanelContent']/div/div/div/div/div/section/div[2]")).Click();
            new Actions(driver).DoubleClick(driver.FindElement(By.XPath("//div[@class='vanSidePanelContent']/div/div/div/div/div/section/div[2]"))).Perform();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//button[@title='Data']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div[@class='vanSidePanelContent']/div/div/div/div/div/section[4]/div[2]/div/ul/li[1]")).Click();
            new Actions(driver).DoubleClick(driver.FindElement(By.XPath("//div[@class='vanSidePanelContent']/div/div/div/div/div/section[4]/div[2]/div/ul/li[1]"))).Perform();
            Thread.Sleep(3000);
            Automation.GetScreenshot(driver, Reportfilepath + "reports.png");
            TestContext.AddTestAttachment(Reportfilepath + "reports.png");
        }

        [Test, Order(2)]
        [TestCase(TestName = "Create a map Report")]
        public void MapReport()
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
            driver.FindElement(By.Id("VANextLogon_iframe"));
            driver.SwitchTo().Frame(driver.FindElement(By.Id("VANextLogon_iframe")));

            Automation.WaitUntilElementExists(driver, "//div[@id='VANextLogon']", 60);
            Thread.Sleep(3000);
            if (Automation.WaitUntilElementExists(driver, "//div[@role='alertdialog']", 60))
            {
                Thread.Sleep(3000);
                driver.FindElement(By.XPath("//div[@id='__dialog0-footer']/button[2]")).Click();
                Thread.Sleep(3000);
            }
            Automation.WaitUntilElementExists(driver, "//div[@id='_vaQuickStartDialog-popover-cont']", 60);
            Thread.Sleep(3000);

            new Actions(driver).SendKeys(Keys.Escape).Perform();
            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//button[@title='Data']")).Click();
            Thread.Sleep(6000);
            string idDemographics = driver.FindElement(By.XPath("//div[@aria-label='Actions for DEMOGRAPHICS']")).GetAttribute("id");
            driver.FindElement(By.Id(idDemographics.Replace("-actionToolbar", ""))).Click();
            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//button/span/span/bdi[text()='OK']")).Click();
            Thread.Sleep(3000);
            new Actions(driver).SendKeys(Keys.Escape).Perform();
            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//button[@title='Objects']")).Click();
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//ul[@id='__vanlist2-listUl']/li[9]")).Click();
            new Actions(driver).DoubleClick(driver.FindElement(By.XPath("//ul[@id='__vanlist2-listUl']/li[9]"))).Perform();
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//button[@title='Data']")).Click();
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//div[@id='__shade0']//section/div[2]/div/ul/li[1]")).Click();
            new Actions(driver).DoubleClick(driver.FindElement(By.XPath("//div[@id='__shade0']//section/div[2]/div/ul/li[1]"))).Perform();
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//div[@role='toolbar']/button")).Click();
            Thread.Sleep(30000);

            driver.FindElement(By.XPath("//button[@id='vanApplicationToolbar-save']")).Click();
            Thread.Sleep(8000);

            driver.FindElement(By.XPath("//footer/div/button")).Click();
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//div[@role='alertdialog']/footer/div/button")).Click();
            Thread.Sleep(6000);

            Automation.GetScreenshot(driver, Reportfilepath + "georeports.png");
            TestContext.AddTestAttachment(Reportfilepath + "georeports.png");
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
