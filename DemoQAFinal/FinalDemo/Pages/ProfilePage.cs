using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalDemo.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace FinalDemo.Pages
{
    public class ProfilePage : BasePage
    {
        private Element _userName = new Element(By.Id("userName"));
        private Element _passWord = new Element(By.Id("password"));
        private Element _btnLogin = new Element(By.Id("login"));
        private Element _bookName(string bookTitle)
        {
            return new Element(By.XPath($"//a[contains(text(),'{bookTitle}')]"));
        }
        private Element _searchBox = new Element(By.Id("searchBox"));
        public Element _btnDelete(string bookTitle)
        {
            return new Element(By.XPath($"//a[text()='{bookTitle}']/ancestor::div[@role='gridcell']/following-sibling::div//span[@id='delete-record-undefined']"));
        }
        private Element _btnOK = new Element(By.Id("closeSmallModal-ok"));

        public void DeleteBook(string bookTitle)
        {
            if (CheckBookExist(bookTitle))
            {
                _btnDelete(bookTitle).ClickOnElement();
                _btnOK.ClickOnElement();
                CloseAlert();

            }
        }
        public void CloseAlert()
        {
            WebDriverWait wait = new WebDriverWait(DriverManager.WebDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = DriverManager.WebDriver.SwitchTo().Alert();
            alert.Accept();
        }
        public bool CheckBookExist(string bookTitle)
        {
            return (_bookName(bookTitle)).WaitForElementToBeVisible() != null;
        }
    }
}
