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
    public class SearchBookTest : BaseTest
    {
        private SearchBookPage _searchBookPage;

        [SetUp]
        public void PageSetUp()
        {
            _searchBookPage = new SearchBookPage();
            DriverManager.GoToUrl(ConfigurationHelper.GetConfiguration()["searchBookURL"]);
        }


        [Test]
        [TestCase("design")]
        [TestCase("Design")]
        [Category("SearchBook")]
        public void SearchBook(string criteria)
        {
            ExtentReportHelper.LogTestStep("1. Search book " + criteria);
            _searchBookPage.SearchBookWithMultiResults(criteria);

            ExtentReportHelper.LogTestStep("2. Verify search results");
            _searchBookPage.AssertSearchResults(criteria);
        }
    }
}

