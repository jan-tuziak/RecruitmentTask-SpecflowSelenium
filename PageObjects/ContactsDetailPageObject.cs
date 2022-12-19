using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentTaskSpecflowSelenium.PageObjects
{
    internal class ContactsDetailPageObject
    {
        //The Selenium web driver to automate the browser
        private readonly IWebDriver _webDriver;

        //The default wait time in seconds for wait.Until
        public const int DefaultWaitInSeconds = 10;

        WebDriverWait webDriverWait;

        public ContactsDetailPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
        }

        //Finding elements by ID and Css
        private IWebElement FullNameHeader => _webDriver.FindElement(By.CssSelector("div#_form_header>h3"));
        private IWebElement BusinessRoleHeader => _webDriver.FindElement(By.CssSelector("div.cell-business_role>div.form-entry>div.form-value"));
        private IWebElement CategoriesList => _webDriver.FindElement(By.CssSelector("div.summary-right>div.summary-meta>ul>li:first-child"));

        internal void CheckNewContact(string firstName, string lastName, string role, string category1, string category2)
        {
            string actualFullName = FullNameHeader.Text;
            string actualBusinessRole = BusinessRoleHeader.Text;
            string actualCategories = CategoriesList.Text;

            actualFullName.Should().Contain($"{firstName} {lastName}");
            actualBusinessRole.Should().Be(role);
            actualCategories.Should().Contain(category1);
            actualCategories.Should().Contain(category2);
        }

    }
}
