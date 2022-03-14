using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using System.Threading;

namespace SASViya4Test
{
    [TestFixture]
    public class SASViyaStudioTest
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

            url = TestContext.Parameters.Get("SASStudioUrl");
            folderPath = TestContext.Parameters.Get("screenshotFilepath");
            Validationfilepath = folderPath + "/SASStudio/";
        }

        [Test, Order(1)]
        [TestCase(TestName = "Submit a SAS code")]
        public void CodeExecution()
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
            driver.FindElement(By.Id("sasstudio_iframe"));
            driver.SwitchTo().Frame(driver.FindElement(By.Id("sasstudio_iframe")));

            Automation.WaitUntilElementExists(driver, "//div[@id='sasstudio']", 60);
            Thread.Sleep(3000);
            Automation.WaitUntilElementExists(driver, "//div[@id='SASStudio--newMenuButton']", 30);
            
            Thread.Sleep(10000);
            driver.FindElement(By.Id("SASStudio--newMenuButton-internalBtn-BDI-content")).Click();
            driver.FindElement(By.Id("SASStudio--newProgramStandardMenuItem-unifiedmenu-txt")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.CssSelector(".sce-sas .textviewContent")).Click();
            driver.FindElement(By.CssSelector(".sce-sas .textviewContent")).SendKeys("proc print data=sashelp.class;run;");
            Thread.Sleep(6000);
            driver.FindElement(By.Id("__jsview6--program1--codeSubmitButton1")).Click();
            Thread.Sleep(6000);
            Automation.GetScreenshot(driver, Validationfilepath + "code1.png");
            TestContext.AddTestAttachment(Validationfilepath + "code1.png");

            Thread.Sleep(5000);
            driver.FindElement(By.Id("SASStudio--newMenuButton-internalBtn-BDI-content")).Click();
            driver.FindElement(By.Id("SASStudio--newProgramStandardMenuItem-unifiedmenu-txt")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.CssSelector(".sce-sas .textviewContent")).Click();
            driver.FindElement(By.CssSelector(".sce-sas .textviewContent")).SendKeys("cas; caslib _all_ assign;");
            Thread.Sleep(6000);
            driver.FindElement(By.Id("__jsview6--program2--codeSubmitButton1")).Click();
            Thread.Sleep(6000);
            Automation.GetScreenshot(driver, Validationfilepath + "code2.png");
            TestContext.AddTestAttachment(Validationfilepath + "code2.png");

            Thread.Sleep(5000);
            driver.FindElement(By.Id("SASStudio--newMenuButton-internalBtn-BDI-content")).Click();
            driver.FindElement(By.Id("SASStudio--newProgramStandardMenuItem-unifiedmenu-txt")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.CssSelector(".sce-sas .textviewContent")).Click();
            driver.FindElement(By.CssSelector(".sce-sas .textviewContent")).SendKeys("cas; caslib _all_ assign; libname locallib \"!SASROOT/samples/base\"; proc casutil; load data=locallib.hmeq outcaslib=casuser casout=\"hmeq\" replace; run;");
            Thread.Sleep(6000);
            driver.FindElement(By.Id("__jsview6--program3--codeSubmitButton1")).Click();
            Thread.Sleep(6000);
            Automation.GetScreenshot(driver, Validationfilepath + "code3.png");
            TestContext.AddTestAttachment(Validationfilepath + "code3.png");

            Thread.Sleep(5000);
            driver.FindElement(By.Id("SASStudio--newMenuButton-internalBtn-BDI-content")).Click();
            driver.FindElement(By.Id("SASStudio--newProgramStandardMenuItem-unifiedmenu-txt")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.CssSelector(".sce-sas .textviewContent")).Click();
            driver.FindElement(By.CssSelector(".sce-sas .textviewContent")).SendKeys("proc print data=casuser.hmeq(obs=10); run;");
            Thread.Sleep(6000);
            driver.FindElement(By.Id("__jsview6--program4--codeSubmitButton1")).Click();
            Thread.Sleep(6000);
            Automation.GetScreenshot(driver, Validationfilepath + "code4.png");
            TestContext.AddTestAttachment(Validationfilepath + "code4.png");

            Thread.Sleep(5000);
            driver.FindElement(By.Id("SASStudio--newMenuButton-internalBtn-BDI-content")).Click();
            driver.FindElement(By.Id("SASStudio--newProgramStandardMenuItem-unifiedmenu-txt")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.CssSelector(".sce-sas .textviewContent")).Click();
            driver.FindElement(By.CssSelector(".sce-sas .textviewContent")).SendKeys("cas; caslib _all_ assign; proc casutil; load data=sashelp.prdsale outcaslib=public casout=\"prdsale\" replace; run;");
            Thread.Sleep(6000);
            driver.FindElement(By.Id("__jsview6--program5--codeSubmitButton1")).Click();
            Thread.Sleep(6000);
            Automation.GetScreenshot(driver, Validationfilepath + "code5.png");
            TestContext.AddTestAttachment(Validationfilepath + "code5.png");
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
