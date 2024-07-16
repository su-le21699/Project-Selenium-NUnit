using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V125.Page;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace FinalDemo.Core
{
    public class Element
    {
        public By By { get; set; }

        public Element(By by)
        {
            By = by;
        }

        public IWebElement WaitForElementToBeVisible()
        {

            var wait = new WebDriverWait(DriverManager.WebDriver, TimeSpan.FromSeconds(10));
            try
            {
                return wait.Until(ExpectedConditions.ElementIsVisible(By));
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }

        }
        public IWebElement WaitForElementToBeClickable()
        {
            var wait = new WebDriverWait(DriverManager.WebDriver, TimeSpan.FromSeconds(15));
            return wait.Until(ExpectedConditions.ElementIsVisible(By));
        }
        public void JavaScriptClick()
        {
            var element = WaitForElementToBeVisible();
            if (element == null)
            {
                throw new NoSuchElementException($"Element not found: {By}");
            }
            ((IJavaScriptExecutor)DriverManager.WebDriver).ExecuteScript("arguments[0].click();", element);
        }
        public void ScrollToView()
        {
            var element = WaitForElementToBeVisible();
            if (element == null)
            {
                throw new NoSuchElementException($"Element not found: {By}");
            }
            ((IJavaScriptExecutor)DriverManager.WebDriver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        /*
        public void ClickOnElement()
        {
            ScrollToView();
            IWebElement element = WaitForElementToBeVisible();
            element.Click();
        }
        */
        public void ClickOnElement()
        {
            try
            {
                ScrollToView();
                var element = WaitForElementToBeClickable();
                if (element == null)
                {
                    throw new NoSuchElementException($"Element not clickable: {By}");
                }
                element.Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Standard click failed for element: {By}. Error: {ex.Message}. Trying JavaScript click.");
                JavaScriptClick();
            }
        }

        // public void EnterText(string value)
        // {
        //     ScrollToView();
        //     IWebElement element = WaitForElementToBeVisible();
        //     if (element == null)
        //     {
        //         throw new NoSuchElementException($"Element not found for entering text: {By}");
        //     }
        //     element.SendKeys(value);
        // }
        public void EnterText(string value)
        {
            IWebElement element = WaitForElementToBeVisible();
            element.SendKeys(value);
        }
        public string GetTextFromElement()
        {
            return WaitForElementToBeVisible().Text;
        }

        public void SelectByText(string text)
        {
            ScrollToView();
            IWebElement element = WaitForElementToBeVisible();
            if (element == null)
            {
                throw new NoSuchElementException($"Dropdown element not found: {By}");
            }
            var selectElement = new SelectElement(element);
            selectElement.SelectByText(text);
        }
        public void ScrollToElement()
        {
            IWebElement element = WaitForElementToBeVisible();
            IJavaScriptExecutor js = (IJavaScriptExecutor)DriverManager.WebDriver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }
        public void ClearText()
        {
            IWebElement element = WaitForElementToBeVisible();
            element.Clear();
        }
        public void ClickByJS()
        {
            IWebElement element = WaitForElementToBeVisible();
            IJavaScriptExecutor js = (IJavaScriptExecutor)DriverManager.WebDriver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            element.Click();
        }
    }
}