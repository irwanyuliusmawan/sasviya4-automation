using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Diagnostics;
using System.Threading;

namespace SASViya4Test
{
    public class SASViyaLineageTest : BaseClass
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
            url = TestContext.Parameters.Get("SASLineageUrl");
            folderPath = TestContext.Parameters.Get("screenshotFilepath");
            driver = GetDriver();
            Validationfilepath = GetLocalPath() + GetScreenShotPath();
        }

        [Test, Order(1), Category("Playlist2")]
        [TestCase(TestName = "Display the relationship of a simple component")]
        public void Report()
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
            driver.FindElement(By.Id("lineage_app_iframe"));
            driver.SwitchTo().Frame(driver.FindElement(By.Id("lineage_app_iframe")));

            Automation.WaitUntilElementExists(driver, "//div[@id='lineage_app']", 60);
            Thread.Sleep(3000);
            
            driver.FindElement(By.XPath("//div[@class='sasMZeroStateButtonGroup']/button[2]")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//input[@type='search']")).SendKeys("CSV");
            Thread.Sleep(8000);
            driver.FindElement(By.XPath("//button[@title='Start search']")).Click();
            Thread.Sleep(4000);
            new Actions(driver).Click(driver.FindElement(By.XPath("//div[@aria-label='Row 1, row header.']/div/div/input"))).Perform();
            driver.FindElement(By.XPath("//footer/div/button[1]")).Click();
            Thread.Sleep(18000);
            
            Automation.GetScreenshot(driver, Validationfilepath + "reports.png", env);
            TestContext.AddTestAttachment(Validationfilepath + "reports.png");
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
