using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Diagnostics;
using System.Threading;

namespace SASViya4Test
{
    public class SASViyaEnvironmentManagerTest : BaseClass
    {
        IWebDriver driver;

        string env;
        string url;
        string Loginfilepath;
        string ValidationFolderfilepath;
        string AccessDataPagefilepath;
        string AccessServerPagefilepath;
        string importDemographics;
        string testfile;
        string testfile2;
        public TestContext TestContext { get; set; }

        [SetUp]
        public void SetUp()
        {
            env = TestContext.Parameters.Get("environment");
            url = TestContext.Parameters.Get("SASEnvMgrUrl");
            testfile = TestContext.Parameters.Get("testFilePath");
            testfile2 = TestContext.Parameters.Get("demographicfilepath");
            driver = GetDriver();
            Loginfilepath = GetLocalPath() + GetScreenShotPath();
            ValidationFolderfilepath = GetLocalPath() + GetScreenShotPath();
            AccessDataPagefilepath = GetLocalPath() + GetScreenShotPath();
            AccessServerPagefilepath = GetLocalPath() + GetScreenShotPath();
            importDemographics = GetLocalPath() + GetScreenShotPath();
        }

       
        [Test, Order(1), Category("Playlist1")]
        [TestCase(TestName = "Login to Environment Manager")]
        public void Login()
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Size = new System.Drawing.Size(1280, 680);
            string username = TestContext.Parameters.Get("username");
            string password = TestContext.Parameters.Get("password");
            driver.FindElement(By.Id("username")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Id("submitBtn")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.Id("sas-admin")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.Id("envmgrapp_iframe"));
            driver.SwitchTo().Frame(driver.FindElement(By.Id("envmgrapp_iframe")));
            
            if(Automation.WaitUntilElementExists(driver, "//div[@id='welcomeScreen']", 30))
            {
                IWebElement element = driver.FindElement(By.XPath("//div[@id='welcomeScreen']"));
                if (element != null)
                {
                    driver.FindElement(By.XPath("//div[@id='welcomeScreen']/div[2]/div/div/div/div/button")).Click();
                    Thread.Sleep(3000);
                    driver.FindElement(By.XPath("//a[@id='skipLink']")).Click();
                    Thread.Sleep(3000);
                    driver.FindElement(By.XPath("//div[@id='welcomeScreen']/div/div/div/div/div[5]/div[2]/button")).Click();
                    Thread.Sleep(3000);
                }
            }
            else
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);
            }

            driver.FindElement(By.Id("envmgrapp_appContainer_lfn_4_icn")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.Id("ev_identity_typeFilter-label")).Click();
            driver.FindElement(By.XPath("//ul/li[1]")).Click();
            Thread.Sleep(3000);
            Automation.GetScreenshot(driver,Loginfilepath + "users.png", env);
            TestContext.AddTestAttachment(Loginfilepath + "users.png");
            driver.FindElement(By.Id("ev_identity_typeFilter-label")).Click();
            driver.FindElement(By.XPath("//ul/li[2]")).Click();
            Thread.Sleep(3000);
            Automation.GetScreenshot(driver, Loginfilepath + "groups.png", env);
            TestContext.AddTestAttachment(Loginfilepath + "groups.png");
            driver.FindElement(By.Id("ev_identity_typeFilter-label")).Click();
            driver.FindElement(By.XPath("//ul/li[3]")).Click();
            Thread.Sleep(3000);
            Automation.GetScreenshot(driver, Loginfilepath + "customegroup.png", env);
            TestContext.AddTestAttachment(Loginfilepath + "customegroup.png");
        }

        [Test, Order(2), Category("Playlist1")]
        [TestCase(TestName = "Create a validation folder")]
        public void ValidateValidationFolder()
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
            driver.FindElement(By.Id("envmgrapp_iframe"));
            driver.SwitchTo().Frame(driver.FindElement(By.Id("envmgrapp_iframe")));

            Automation.WaitUntilElementExists(driver, "//div[@id='envmgrapp']", 60);
            Thread.Sleep(6000);

            driver.FindElement(By.Id("envmgrapp_appContainer_lfn_3_icn")).Click();
            Thread.Sleep(6000);
            driver.FindElement(By.Id("ContentSelectionPane-basic-cntntSelPaneListItemPrefix-ContentSelectionPane-basic-cntntSelPane-list0-2-imgNav")).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.Id("ContentSelectionPane-basic-cntntSelPane-toolbar-newFolderButton-img")).Click();
            char c = '\u0001'; // ASCII code 1 for Ctrl-A
            driver.FindElement(By.CssSelector(".sasContentNavNewFolderInput")).SendKeys(Convert.ToString(c));
            Random rnd = new Random();
            int num = rnd.Next();
            string input = "Install Validaton" + num.ToString();
            driver.FindElement(By.CssSelector(".sasContentNavNewFolderInput")).SendKeys(input);
            driver.FindElement(By.CssSelector(".sasContentNavNewFolderInput")).SendKeys(Keys.Enter);
            Thread.Sleep(8000);
            Automation.GetScreenshot(driver, ValidationFolderfilepath + "folders.png", env);
            Thread.Sleep(8000);
            driver.FindElement(By.Id("sasev_content_AuthButton-internalBtn-img")).Click();
            TestContext.AddTestAttachment(ValidationFolderfilepath + "folders.png");
            Thread.Sleep(8000);
            driver.FindElement(By.Id("sasev_content_edit_auth_toolbar_menuitem-unifiedmenu-txt")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Thread.Sleep(4000);
            Automation.GetScreenshot(driver, ValidationFolderfilepath + "authentication.png", env);
            driver.FindElement(By.Id("sas-ev-shared-authorization-AuthorizationUIFactory-authDialog-closeXButton-img")).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.Id("ContentSelectionPane-basic-cntntSelPane-toolbar-DeleteButton-img")).Click();
            Thread.Sleep(4000);
            TestContext.AddTestAttachment(ValidationFolderfilepath + "authentication.png");
        }

        [Test, Order(3), Category("Playlist1")]
        [TestCase(TestName = "Access Data page")]
        public void AccessDataPage()
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
            driver.FindElement(By.Id("envmgrapp_iframe"));
            driver.SwitchTo().Frame(driver.FindElement(By.Id("envmgrapp_iframe")));

            Automation.WaitUntilElementExists(driver, "//div[@id='envmgrapp']", 60);
            Thread.Sleep(8000);

            driver.FindElement(By.CssSelector("#\\__xmlview3--importTabFilter > .sapMITBContentArrow")).Click();
            Thread.Sleep(6000);
            driver.FindElement(By.CssSelector("#\\__xmlview3--availableTabFilter > .sapMITBContentArrow")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.CssSelector("#\\__xmlview3--importTabFilter > .sapMITBContentArrow")).Click();
            driver.FindElement(By.CssSelector("#\\__xmlview9--localFileListItem-content .sapMSLITitleOnly")).Click();
            Thread.Sleep(5000);
            //driver.FindElement(By.Id("__item787-unifiedmenu-txt")).Click();
            driver.FindElement(By.Id("__xmlview9--localFileUploader-fu")).SendKeys(testfile);
            Thread.Sleep(5000);
            driver.FindElement(By.CssSelector("#\\__xmlview2--importSettings--replaceExistingButton-label > .sasMLabelText")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("__xmlview2--importSelectedButton-BDI-content")).Click();
            Thread.Sleep(3000);
            Automation.GetScreenshot(driver, AccessDataPagefilepath + "fileimport.png", env);
            TestContext.AddTestAttachment(AccessDataPagefilepath + "fileimport.png");
            driver.FindElement(By.CssSelector("#\\__xmlview3--availableTabFilter > .sapMITBContentArrow")).Click();
            var xpath = string.Format("(//div[@id='__xmlview7--availableList-listContent']/ul/li)[1]");
            driver.FindElement(By.XPath(xpath)).Click();
            Thread.Sleep(5000);
            Automation.GetScreenshot(driver, AccessDataPagefilepath + "filedata.png", env);
            TestContext.AddTestAttachment(AccessDataPagefilepath + "filedata.png");
        }

        [Test, Order(4), Category("Playlist1")]
        [TestCase(TestName = "Access Servers page")]
        public void AccessServerPage()
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
            driver.FindElement(By.Id("envmgrapp_iframe"));
            driver.SwitchTo().Frame(driver.FindElement(By.Id("envmgrapp_iframe")));

            Automation.WaitUntilElementExists(driver, "//div[@id='envmgrapp']", 60);
            Thread.Sleep(6000);

            driver.FindElement(By.Id("envmgrapp_appContainer_lfn_2_icn")).Click();
            Thread.Sleep(6000);
            Automation.ElementEnabled(driver.FindElement(By.Id("EVNextCasServersViewPage--sasev_casserver_list-rows-row0-col0")));
            //Automation.WaitUntilElementExists(driver, By.Id("EVNextCasServersViewPage--sasev_casserver_list-rows-row0-col0"), 20);
            Thread.Sleep(5000);
            driver.FindElement(By.Id("EVNextCasServersViewPage--sasev_casserver_list-rows-row0-col0")).Click();
            Thread.Sleep(3000);
            Automation.GetScreenshot(driver, AccessServerPagefilepath + "casserver.png", env);
            TestContext.AddTestAttachment(AccessServerPagefilepath + "casserver.png");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("EVNextCasServersViewPage--sasev_casserver_configurationButton-img")).Click();
            driver.FindElement(By.CssSelector("#\\__filter2 > .sapMITBContentArrow")).Click();
            Thread.Sleep(2000);
            Automation.GetScreenshot(driver, AccessServerPagefilepath + "casnodes.png", env);
            TestContext.AddTestAttachment(AccessServerPagefilepath + "casnodes.png");
        }

        [Test, Order(5), Category("Playlist1")]
        [TestCase(TestName = "Import Demographics")]
        public void ImportDemographic()
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
            driver.FindElement(By.Id("envmgrapp_iframe"));
            driver.SwitchTo().Frame(driver.FindElement(By.Id("envmgrapp_iframe")));

            Automation.WaitUntilElementExists(driver, "//div[@id='envmgrapp']", 60);
            Thread.Sleep(8000);

            driver.FindElement(By.CssSelector("#\\__xmlview3--importTabFilter > .sapMITBContentArrow")).Click();
            Thread.Sleep(6000);
            driver.FindElement(By.CssSelector("#\\__xmlview3--availableTabFilter > .sapMITBContentArrow")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.CssSelector("#\\__xmlview3--importTabFilter > .sapMITBContentArrow")).Click();
            driver.FindElement(By.CssSelector("#\\__xmlview9--localFileListItem-content .sapMSLITitleOnly")).Click();
            Thread.Sleep(5000);
            //driver.FindElement(By.Id("__item787-unifiedmenu-txt")).Click();
            driver.FindElement(By.Id("__xmlview9--localFileUploader-fu")).SendKeys(testfile2);
            Thread.Sleep(5000);
            driver.FindElement(By.CssSelector("#\\__xmlview2--importSettings--replaceExistingButton-label > .sasMLabelText")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("__xmlview2--importSelectedButton-BDI-content")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.CssSelector("#\\__xmlview3--availableTabFilter > .sapMITBContentArrow")).Click();
            var xpath = string.Format("(//div[@id='__xmlview7--availableList-listContent']/ul/li)[1]");
            driver.FindElement(By.XPath(xpath)).Click();
            Thread.Sleep(5000);
            Automation.GetScreenshot(driver, importDemographics + "import.png", env);
            TestContext.AddTestAttachment(importDemographics + "import.png");
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
