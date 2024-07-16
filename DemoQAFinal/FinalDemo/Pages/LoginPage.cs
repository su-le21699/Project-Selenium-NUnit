using FinalDemo.Core;
using OpenQA.Selenium;

namespace FinalDemo.Pages
{
    public class LoginPage : BasePage
    {
        private Element _txtUserName = new(By.Id("userName"));
        private Element _txtPassword = new(By.Id("password"));
        private Element _btnLogin = new(By.Id("login"));
        private Element _btnRegister = new Element(By.Id("newUser"));
        public void Login(string username, string password)
        {
           
            _txtUserName.EnterText(username);
            _txtPassword.EnterText(password);
            _btnLogin.ClickOnElement();
        }
    }
}