using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalDemo.Core;

namespace FinalDemo.Test
{
    [TestFixture]
    [TestFixture, Parallelizable(ParallelScope.Fixtures)]
    public class BaseTest
    {

        public BaseTest()
        {
            ExtentReportHelper.CreateTest(TestContext.CurrentContext.Test.ClassName);
        }

        [SetUp]
        public void Setup()
        {
            string browser = ConfigurationHelper.GetConfiguration()["browser"];
            double timeOutSec = double.Parse(ConfigurationHelper.GetConfiguration()["timeout.webdriver.wait.seconds"]);


            ExtentReportHelper.CreateNode(TestContext.CurrentContext.Test.Name);
            ExtentReportHelper.LogTestStep("Initialize webdriver");

            DriverManager.InitDriver(browser);
            DriverManager.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeOutSec);
            DriverManager.WebDriver.Manage().Window.Maximize();
            Console.WriteLine(TestContext.Parameters["Base Test set up"]);
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
            ? ""
            : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            ExtentReportHelper.CreateTestResult(status, stacktrace, TestContext.CurrentContext.Test.ClassName, TestContext.CurrentContext.Test.Name, DriverManager.WebDriver);
            ExtentReportHelper.Flush();
            DriverManager.CloseDriver();
            Console.WriteLine("Base Test tear down");
        }
    }
}