using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace SASViya4Test
{
    internal class Automation
    {
        public static bool WaitUntilElementExists(IWebDriver Driver, string xPath, int timeout = 10)
        {
            try
            {
                new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout)).Until(ExpectedConditions.ElementExists(By.XPath(xPath)));
                return true;
            }
            catch
            {
               return false;
            }
        }

        public static IWebElement? ElementEnabled(IWebElement element, int timeout = 10000)
        {
            var s = new Stopwatch();
            s.Start();

            while (s.Elapsed < TimeSpan.FromMilliseconds(timeout))
            {
                if (element.Enabled)
                {
                    return element;
                }
            }

            s.Stop();
            return null;
        }

        public static void GetScreenshot(IWebDriver driver, string filePath, string env)
        {
            if (env == "Linux")
            {
                ITakesScreenshot screenshot;
                screenshot = driver as ITakesScreenshot;
                Screenshot screenshot1 = screenshot.GetScreenshot();
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                screenshot1.SaveAsFile(filePath, ScreenshotImageFormat.Png);
            }
            else
            {
                ITakesScreenshot screenshot;
                screenshot = driver as ITakesScreenshot;
                Screenshot screenshot1 = screenshot.GetScreenshot();
                string TempFilePath = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".png";
                screenshot1.SaveAsFile(TempFilePath, ScreenshotImageFormat.Png);

                using (Image image = Image.FromFile(TempFilePath, true))
                {
                    // Declare a String object with Watermark Text
                    DateTime dt = DateTime.Now;
                    string theString = dt.ToString("G", DateTimeFormatInfo.InvariantInfo);

                    // Create and initialize an instance of Graphics class and Initialize an object of SizeF to store image Size
                    int width = image.Width;
                    int height = image.Height;
                    int font_size = (width > height ? height : width) / 35;
                    Point text_starting_point = new Point(height / 15, (width / 160));
                    Font text_font = new Font("Arial", font_size, FontStyle.Bold, GraphicsUnit.Pixel);
                    Color color = Color.FromArgb(255, 255, 255, 0);
                    SolidBrush brush = new SolidBrush(color);
                    Graphics graphics = Graphics.FromImage(image);
                    graphics.DrawString(theString, text_font, brush, text_starting_point);
                    graphics.Dispose();

                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    image.Save(filePath);
                    image.Dispose();
                }
            }
        }
    }
}
