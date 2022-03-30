using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Diagnostics;
using System.Threading;

namespace SASViya4Test
{
    public class SASViyaDataStudioTest : BaseClass
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
            url = TestContext.Parameters.Get("SASDataStudioUrl");
            driver = GetDriver();
            Validationfilepath = GetLocalPath() + GetScreenShotPath();
        }

        [Test, Order(1), Category("Playlist2")]
        [TestCase(TestName = "Add new calculated column to a dataset")]
        public void AddNewColumn()
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
            driver.FindElement(By.Id("data_wrangler_ui_iframe"));
            driver.SwitchTo().Frame(driver.FindElement(By.Id("data_wrangler_ui_iframe")));

            Automation.WaitUntilElementExists(driver, "//div[@id='data_wrangler_ui']", 80);
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//div[@class='sasMZeroStateButtonGroup']/button[1]")).Click();
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//div[@role='tabpanel']/div/div[2]/div/ul/li[1]")).Click();
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//button[@data-landmark-label='Choose Data OK button']")).Click();
            Thread.Sleep(8000);

            driver.FindElement(By.XPath("//div[@id='contentView--transformContainer--transformShadesContainer']/section[2]/div[2]/div/div/ul/li[1]")).Click();
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//div[@id='contentView--transformContainer']/div/div/button")).Click();
            Thread.Sleep(12000);

            driver.FindElement(By.XPath("//div[@class='textviewContent']")).Click();
            driver.FindElement(By.XPath("//div[@class='textviewContent']")).SendKeys("age+1");
            Thread.Sleep(6000);

            //new Actions(driver).Click(driver.FindElement(By.XPath("//div[@role='radio' and @aria-posinset='1']/div[@class='sapMRbB']/div/div/input"))).Perform();
            //driver.FindElement(By.Id("__button49-__xmlview24--targetColumnsList-0-Button")).Click();
            //new Actions(driver).Click(driver.FindElement(By.XPath("//input[@type='radio']"))).Perform();
            //Thread.Sleep(5000);
            //new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            //Thread.Sleep(5000);

            new Actions(driver).Click(driver.FindElement(By.XPath("//div[@class='sapMRbB']/div"))).Perform();
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            Thread.Sleep(6000);
            //div[@class='sapMRbB']/div

            driver.FindElement(By.XPath("//header[@class='sapMPageHeader']/div/div[3]/div/div/button")).Click();
            Thread.Sleep(15000);

            Automation.GetScreenshot(driver, Validationfilepath + "dataset.png", env);
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
