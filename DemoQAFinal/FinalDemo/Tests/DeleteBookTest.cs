using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalDemo.Core;
using FinalDemo.Pages;
using FinalDemo.Test;

namespace FinalDemo.Tests
{
    [TestFixture]
    public class DeleteBookTest : BaseTest
    {
        private ProfilePage _profilePage;
        private LoginPage _loginPage;
        [SetUp]
        public void PageSetUp()
        {
            _profilePage = new ProfilePage();
            _loginPage = new LoginPage();
            DriverManager.GoToUrl(ConfigurationHelper.GetConfiguration()["loginULR"]);
        }
        [Test]
        [Category("DeleteBook")]
        [TestCase("Git Pocket Guide")]
        public void DeleteBookOnProfilePage(string bookTitle)
        {
            ExtentReportHelper.LogTestStep("1. Login successfully with valid account");
            _loginPage.Login(Constant.USERNAME, Constant.PASSWORD);

            ExtentReportHelper.LogTestStep("2. Delete book " + bookTitle);
            _profilePage.DeleteBook(bookTitle);

            ExtentReportHelper.LogTestStep("3. Verify delete result ");
            bool check = _profilePage.CheckBookExist(bookTitle);
            Assert.That(check, Is.EqualTo(false));
        }
    }
}