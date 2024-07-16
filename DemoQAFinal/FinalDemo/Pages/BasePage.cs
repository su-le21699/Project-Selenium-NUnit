using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalDemo.Core;
using OpenQA.Selenium;

namespace FinalDemo.Pages
{
    public class BasePage
    {
        public IWebDriver WebDriver;
        
        protected BasePage()
        {
            WebDriver = DriverManager.WebDriver;
          
        }
        public void GotoUrl(string url)
        {
            WebDriver.Url = url;
        }
    }
}