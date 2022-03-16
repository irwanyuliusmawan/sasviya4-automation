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
    public class SASViyaJobFlowScheulerTest
    {
        IWebDriver driver;

        string env;
        string url;
        string envurl;
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

            url = TestContext.Parameters.Get("SASStudioUrl");
            envurl = TestContext.Parameters.Get("SASEnvMgrUrl");
            folderPath = TestContext.Parameters.Get("screenshotFilepath");
            Validationfilepath = folderPath + "/SASJobFlow/";
        }

        [Test, Order(1), Category("Playlist3")]
        [TestCase(TestName = "Save Code")]
        public void ExcuteFlow1()
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

            driver.FindElement(By.Id("SASStudio--newMenuButton-internalBtn-BDI-content")).Click();
            driver.FindElement(By.Id("SASStudio--newProgramStandardMenuItem-unifiedmenu-txt")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.CssSelector(".sce-sas .textviewContent")).Click();
            driver.FindElement(By.CssSelector(".sce-sas .textviewContent")).SendKeys("cas; caslib _all_ assign; proc export data=sashelp.prdsale outfile=\"/tmp/job_flow_scheduler.txt\" dbms=tab replace; run;");
            Thread.Sleep(6000);
            driver.FindElement(By.Id("__jsview6--program1--codeSubmitButton1")).Click();
            Thread.Sleep(6000);
            driver.FindElement(By.XPath("//button[@title='Save']")).Click();
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//ul[@class='sapUiTreeList']/li[1]")).Click();
            Thread.Sleep(3000);
            new Actions(driver).Click(driver.FindElement(By.XPath("//ul[@class='sapUiTreeList']/li[1]"))).Perform();
            Thread.Sleep(3000);
            new Actions(driver).DoubleClick(driver.FindElement(By.XPath("//ul[@class='sapUiTreeList']/li[1]"))).Perform();
            new Actions(driver).DoubleClick(driver.FindElement(By.XPath("//ul[@class='sapUiTreeChildrenNodes']/li[1]"))).Perform();
            new Actions(driver).Click(driver.FindElement(By.XPath("//ul[@class='sapUiTreeChildrenNodes']/li[1]"))).Perform();
            //new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.Enter).Perform();
            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//div[@class='sapUiHLayoutChildWrapper']/button")).Click();
            Thread.Sleep(3000);

            if (Automation.WaitUntilElementExists(driver, "//div[@role='alertdialog']", 30))
            {
                driver.FindElement(By.XPath("//footer/div/button")).Click();
                Thread.Sleep(3000);
            }
        }

        [Test, Order(2), Category("Playlist3")]
        [TestCase(TestName = "Schedule Code")]
        public void ExcuteFlow2()
        {
            driver.Navigate().GoToUrl(envurl);
            driver.Manage().Window.Size = new System.Drawing.Size(1280, 680);
            string username = TestContext.Parameters.Get("username");
            string password = TestContext.Parameters.Get("password");
            driver.FindElement(By.Id("username")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Id("submitBtn")).Click();
            driver.FindElement(By.Id("sas-admin")).Click();

            Thread.Sleep(3000);
            driver.FindElement(By.Id("envmgrapp_iframe"));
            driver.SwitchTo().Frame(driver.FindElement(By.Id("envmgrapp_iframe")));

            Automation.WaitUntilElementExists(driver, "//div[@id='envmgrapp']", 60);
            Thread.Sleep(6000);

            driver.FindElement(By.Id("envmgrapp_appContainer_lfn_12_icn")).Click();
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//div[@role='tab' and @aria-posinset='2']")).Click();
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//button[@id='SASEVScheduleViewPage--sasev_scheduling_new_menu_bnt-internalBtn']")).Click();
            Thread.Sleep(6000);

            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.Enter).Perform();
            Thread.Sleep(6000);

            Random rnd = new Random();
            int num = rnd.Next();
            string input = "Install Validaton" + num.ToString();
            driver.FindElement(By.XPath("//input[@placeholder='Job request name']")).SendKeys(input);
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//button[@title='Open']")).Click();
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//div/ul/li[2]")).Click();
            Thread.Sleep(6000);
            new Actions(driver).DoubleClick(driver.FindElement(By.XPath("//div/ul/li[2]"))).Perform();
            Thread.Sleep(3000);
            //driver.FindElement(By.XPath("//section[@id='__CntSelDlg1-cntntSlctr-columnView-page1-cont']/div/ul/li[1]")).Click();
            //Thread.Sleep(3000);
            new Actions(driver).SendKeys(Keys.Tab).Perform();
            new Actions(driver).SendKeys(Keys.Tab).Perform();
            new Actions(driver).SendKeys(Keys.Enter).Perform();
            Thread.Sleep(3000);
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.Enter).Perform();

            driver.FindElement(By.XPath("//footer/div/button[1]")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//input[@type='search']")).SendKeys(input);
            Thread.Sleep(3000);
            new Actions(driver).SendKeys(Keys.Enter).Perform();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//table[@id='SASEVScheduleViewPage--sasev_jobs_table_list-table']/tbody/tr[1]/td")).Click();
            Thread.Sleep(3000);
            new Actions(driver).ContextClick(driver.FindElement(By.XPath("//table[@id='SASEVScheduleViewPage--sasev_jobs_table_list-table']/tbody/tr[1]/td"))).Perform();
            Thread.Sleep(3000);
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.Enter).Perform();
            Thread.Sleep(3000);

            Automation.GetScreenshot(driver, Validationfilepath + "schedule.png", env);
            TestContext.AddTestAttachment(Validationfilepath + "schedule.png");
            Thread.Sleep(3000);

            new Actions(driver).ContextClick(driver.FindElement(By.XPath("//table[@id='SASEVScheduleViewPage--sasev_jobs_table_list-table']/tbody/tr[1]/td"))).Perform();
            Thread.Sleep(3000);
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.ArrowDown).Perform();
            new Actions(driver).SendKeys(Keys.Enter).Perform();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//footer/div/button[1]")).Click();
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
