using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalDemo.Core;
using FinalDemo.Object;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace FinalDemo.Pages
{
    public class SearchBookPage : BasePage
    {
        private Element _txtSearchBox = new Element(By.Id("searchBox"));
        private Element _btnSearch = new Element(By.CssSelector("[class=input-group-append]"));
        private Element _searchResults = new Element(By.CssSelector("[class='rt-tbody']"));
        private Element _resultRows = new Element(By.CssSelector("[class='rt-tr-group']"));
        private Element _bookTitle(string title)
        {
            return new Element(By.XPath($"//div[contains(@class, 'rt-tr-group')]//span//a[contains(text(), '{title}')]"));
        }

        //Assert the search book result
        public void AssertSearchResults(string criteria)
        {
            List<Books> results = GetBookResults();
            bool bookResults = results.Any(book =>
                book.Title.ToLower().Contains(criteria.ToLower()) || book.Author.ToLower().Contains(criteria.ToLower()) || book.Publisher.ToLower().Contains(criteria.ToLower())
            );
            Assert.That(bookResults, Is.True);

        }
        public void SearchBookWithMultiResults(string criteria)
        {
            _txtSearchBox.EnterText(criteria);
        }
        //Get the columns table header
        public List<Books> GetBookResults()
        {
            List<Books> booksList = new List<Books>();

            List<string> columnNames = new List<string>();
            IList<IWebElement> columnElementList = DriverManager.WebDriver.FindElements(By.XPath("//div[@role='columnheader']"));
            foreach (var column in columnElementList)
            {
                columnNames.Add(column.Text);
            }
            int titleIndex = columnNames.IndexOf("Title");
            int auhthorIndex = columnNames.IndexOf("Author");
            int publisherIndex = columnNames.IndexOf("Publisher");

            //Get rows for search book
            IList<IWebElement> rowElementList = DriverManager.WebDriver.FindElements(By.CssSelector("[role='rowgroup']"));
            foreach (var row in rowElementList)
            {
                Books books = new Books
                {
                    Title = row.FindElements(By.CssSelector("div"))[titleIndex].Text,
                    Author = row.FindElements(By.CssSelector("div"))[auhthorIndex].Text,
                    Publisher = row.FindElements(By.CssSelector("div"))[publisherIndex].Text
                };
                booksList.Add(books);
            }
            return booksList;
        }
    }
}



