using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalDemo.Core;
using FinalDemo.Object;
using FinalDemo.Pages;
using FinalDemo.Test;
using OpenQA.Selenium;

namespace FinalDemo.Tests
{
    [TestFixture]
    public class RegisterStudentTest : BaseTest
    {
        private RegisterStudentPage _registerStudentPage;

        [SetUp]
        public void PageTNSetUp()
        {
            _registerStudentPage = new RegisterStudentPage();
            var config = ConfigurationHelper.GetConfiguration();

            _registerStudentPage.GotoUrl(ConfigurationHelper.GetConfiguration()["registerURL"]);
        }

        [Test]
        [Category("RegisterStudent")]
        [TestCase("StudentAllFields")]
        public void RegisterAllFieldsSuccessfully(string key)
        {
            DataProvider<Student>.Initialize("Data/Student.json");
            var student = DataProvider<Student>.LoadDataByKey(key);

            ExtentReportHelper.LogTestStep("1. Register student with all fields ");
            _registerStudentPage.RegisterStudentAllFields(student);

            ExtentReportHelper.LogTestStep("2. Verify register student result ");
            _registerStudentPage.VerifyRegister(student);
        }
        [Test]
        [Category("RegisterStudent")]
        //[Ignore("skip")]
        [TestCase("StudentMandatoryFields")]
        public void RegisterMandatoryFieldsSuccessfully(string key)
        {
            DataProvider<Student>.Initialize("Data/Student.json");
            var student = DataProvider<Student>.LoadDataByKey(key);

            ExtentReportHelper.LogTestStep("1. Register student with mandatory fields ");
            _registerStudentPage.RegisterStudentMandatoryFields(student);

            ExtentReportHelper.LogTestStep("2. Verify register student result ");
            _registerStudentPage.VerifyRegister(student);
        }
    }
}

