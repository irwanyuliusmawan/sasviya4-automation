using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Diagnostics;
using System.Threading;

namespace SASViya4Test
{
    public class SASViyaConversationDesignerTest : BaseClass
    {
        IWebDriver driver;
        private IJavaScriptExecutor js;

        string env;
        string url;
        string Validationfilepath;
        public TestContext TestContext { get; set; }

        [SetUp]
        public void SetUp()
        {
            env = TestContext.Parameters.Get("environment");
            url = TestContext.Parameters.Get("SASConversationDesignerUrl");
            driver = GetDriver();
            Validationfilepath = GetLocalPath() + GetScreenShotPath();
            js = (IJavaScriptExecutor)driver;
        }

        [Test, Order(1), Category("Playlist3")]
        [TestCase(TestName = "Conversation Designer Related Test Cases")]
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
            Thread.Sleep(5000);
            driver.FindElement(By.Id("ConversationDesignerLogon_iframe"));
            driver.SwitchTo().Frame(driver.FindElement(By.Id("ConversationDesignerLogon_iframe")));

            Automation.WaitUntilElementExists(driver, "//div[@id='ConversationDesignerLogon']", 60); 
            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//div[@id='__bar1-BarLeft']/ul/li[1]")).Click();
            Thread.Sleep(5000);

            Automation.GetScreenshot(driver, Validationfilepath + "login.png", env);
            TestContext.AddTestAttachment(Validationfilepath + "login.png");
            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//div[@id='__bar1-BarRight']/div/button[2]")).Click();
            Thread.Sleep(5000);
            Random rnd = new Random();
            int num = rnd.Next();
            string input = "Install Validaton" + num.ToString();
            driver.FindElement(By.XPath("//div[@class='sasMInputOuterDiv']/input")).SendKeys(input);
            driver.FindElement(By.XPath("//footer/div/button")).Click();
            Thread.Sleep(20000);
            driver.FindElement(By.XPath("//div[@class='sapUiVltCell sapuiVltCell']/div[@role='toolbar']/button")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//input[@value='Dialogue1']")).SendKeys("Welcome user");

            driver.FindElement(By.CssSelector("canvas")).Click();
            Thread.Sleep(12000);
            IWebElement we = driver.FindElement(By.XPath("//canvas"));
            int x = we.Size.Width / 2;
            int y = we.Size.Height / 2;
            new Actions(driver).MoveToElement(driver.FindElement(By.XPath("//canvas")), x, y);
            new Actions(driver).ContextClick(driver.FindElement(By.XPath("//canvas"))).Perform();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//li[@aria-label='Add node above']")).Click();
            driver.FindElement(By.XPath("//li[@aria-label='Event']")).Click();
            driver.FindElement(By.XPath("//li[@aria-label='Start-Chat Event']")).Click();
            Thread.Sleep(5000);
            new Actions(driver).MoveToElement(driver.FindElement(By.XPath("//canvas")), x, y);
            new Actions(driver).ContextClick(driver.FindElement(By.XPath("//canvas"))).Perform();
            driver.FindElement(By.XPath("//li[@aria-label='Add node below']")).Click();
            driver.FindElement(By.XPath("//li[@aria-label='Bot Response']")).Click();
            driver.FindElement(By.XPath("//li[@aria-label='Text Response']")).Click();
            Thread.Sleep(5000);
            IWebElement element = driver.FindElement(By.XPath("//div[@class='textviewContent']"));
            driver.FindElement(By.XPath("//div[@class='textviewContent']")).Click();
            new Actions(driver).SendKeys("Welcome! Can I help you with any SAS questions?.").Perform();
            //js.ExecuteScript("if(arguments[0].contentEditable === 'true') {arguments[0].innerText = 'Welcome! Can I help you with any SAS questions?'}", element);
            Thread.Sleep(8000);
            driver.FindElement(By.XPath("//div[@role='tab' and @aria-posinset='3']")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//div[@class='sapUiVltCell sapuiVltCell']/div[@role='toolbar']/button")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//input[@placeholder='Enter an utterance']")).SendKeys("What is SAS?");
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//section[@class='sapUiLoSplitterContent  ']/div/div[3]/div/div/div/div[2]/div/div[2]/button")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//section[@class='sapUiLoSplitterContent  ']/div/div[3]/div/div/div/div[3]/div/div/div/button[1]")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//div[@role='tab' and @aria-posinset='1']")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//div[@class='sapUiVltCell sapuiVltCell']/div[@role='toolbar']/button")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//input[@value='Dialogue1']")).SendKeys("Open SAS Home Page");
            Thread.Sleep(5000);
            new Actions(driver).MoveToElement(driver.FindElement(By.XPath("//canvas")), x, y);
            new Actions(driver).ContextClick(driver.FindElement(By.XPath("//canvas"))).Perform();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//li[@aria-label='Add node below']")).Click();
            driver.FindElement(By.XPath("//li[@aria-label='Bot Response']")).Click();
            driver.FindElement(By.XPath("//li[@aria-label='HTML Response']")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//div[@class='textviewContent']")).Click();
            IWebElement element2 = driver.FindElement(By.XPath("//div[@class='textviewContent']"));
            driver.FindElement(By.XPath("//div[@class='textviewContent']")).Click();
            new Actions(driver).SendKeys("SAS creates analytics software and solutions.<br><br><a target=\"_blank\" href=\"https://www.sas.com/\">Learn more about SAS</a>").Perform();
            //js.ExecuteScript("if(arguments[0].contentEditable === 'true') {arguments[0].innerHTML = '<div><span class=\" text\">SAS creates analytics software and solutions.</span><span> </span></div><div><span class=\" text\">&lt;br&gt;</span><span> </span></div><div><span class=\" text\">&lt;br&gt;</span><span> </span></div><div><span class=\" text\">&lt;a target=\"_blank\" href=\"https://www.sas.com/\"&gt;Learn more about SAS&lt;/a&gt;</span><span> </span></div><div><span> </span></div>'}", element2);
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//button[@aria-label='Try it now']")).Click();
            Thread.Sleep(8000);
            driver.FindElement(By.XPath("//footer[1]/div/div/div/div/input")).SendKeys("What is SAS?");
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//footer[1]/div/button")).Click();
            Thread.Sleep(8000);
            Automation.GetScreenshot(driver, Validationfilepath + "bot.png", env);
            TestContext.AddTestAttachment(Validationfilepath + "bot.png");

            driver.FindElement(By.XPath("//footer/div/button[2]")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//div[@class='sapMBarContainer sapMBarRight']/button")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//input[@type='search']")).SendKeys(input);
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//div[@id='__bar1-BarLeft']/ul/li[2]")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//div[@role='checkbox' and @aria-label='Row 1, row header.']")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//button[@title='Delete']")).Click();
            Thread.Sleep(8000);
            string buttonid = driver.FindElement(By.XPath("//footer/div/button[1]/span/span/bdi")).GetAttribute("id");
            driver.FindElement(By.Id(buttonid.Replace("-BDI-content",""))).Click();
            Thread.Sleep(8000);
            //driver.FindElement(By.XPath("//div[@id='__bar1-BarLeft']/ul/li[1]")).Click();
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
