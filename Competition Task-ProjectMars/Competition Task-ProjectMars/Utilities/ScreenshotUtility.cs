using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Competition_Task_ProjectMars.Utilities
{
    public class ScreenshotUtility
    {
        public static string CaptureScreenshot(IWebDriver driver, string screenshotName)
        {
            try
            {
                ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;
                Screenshot screenshot = screenshotDriver.GetScreenshot();
                string screenshotPath = Path.Combine("Screenshots", $"{screenshotName}_{DateTime.Now:yyyyMMddHHmmss}.png");
                string fullPath = Path.Combine("C:\\Competition Task-Project Mars\\Project-Mars-Competition-Task\\Competition Task-ProjectMars\\Competition Task-ProjectMars", screenshotPath);
                screenshot.SaveAsFile(fullPath, ScreenshotImageFormat.Png);
                return fullPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error capturing screenshot: " + ex.Message);
                return "";
            }
        }
    }
}
