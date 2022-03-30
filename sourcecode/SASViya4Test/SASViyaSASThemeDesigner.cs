using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using System.Threading;

namespace SASViya4Test
{
    public class SASViyaThemeDesigner : BaseClass
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
            driver = GetDriver();
            url = TestContext.Parameters.Get("SASThemeDesigner");
            Validationfilepath = GetLocalPath() + GetScreenShotPath();
        }

        [Test, Order(1), Category("Playlist1")]
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
            Automation.GetScreenshot(driver, Validationfilepath + "login.png", env);
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
