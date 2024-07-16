using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace FinalDemo.Core
{
    public class DriverManager
    {
        [ThreadStatic]
        public static IWebDriver WebDriver;
        public static void InitDriver(string browserName)
        {
            IWebDriver webDriver = null;
            switch (browserName.ToLower())
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();
                    webDriver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), chromeOptions);
                    break;

                case "edge":
                    var edgeOptions = new EdgeOptions();
                    webDriver = new EdgeDriver(EdgeDriverService.CreateDefaultService(), edgeOptions);
                    break;

                case "firefox":
                    var firefoxOptions = new FirefoxOptions();
                    webDriver = new FirefoxDriver(FirefoxDriverService.CreateDefaultService(), firefoxOptions);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(browserName);

            }
            WebDriver = webDriver;
            WebDriver.Manage().Window.Maximize();
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            WebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
        }
        public static void GoToUrl(string url)
        {
            WebDriver.Url = url;
        }
        public static void CloseDriver()
        {
            WebDriver.Quit();
            WebDriver.Dispose();
        }
    }
}