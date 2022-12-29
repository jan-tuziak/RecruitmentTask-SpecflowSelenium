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
        private readonly IWebDriver driver;

        //The default wait time in seconds for wait.Until
        public const int DefaultWaitInSeconds = 10;

        WebDriverWait wait;

        public ContactsDetailPageObject(IWebDriver webDriver)
        {
            driver = webDriver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
        }

        //Finding elements by ID and Css
        private By FullNameHeader => By.CssSelector("div#_form_header>h3");
        private By BusinessRoleHeader => By.CssSelector("div.cell-business_role>div.form-entry>div.form-value");
        private By CategoriesList => By.CssSelector("div.summary-right>div.summary-meta>ul>li:first-child");

        internal void CheckNewContact(string firstName, string lastName, string role, string category1, string category2)
        {
            wait.Until(driver => CommonElements.Appears(driver, FullNameHeader));
            string actualFullName = driver.FindElement(FullNameHeader).Text;
            string actualBusinessRole = driver.FindElement(BusinessRoleHeader).Text;
            string actualCategories = driver.FindElement(CategoriesList).Text;

            actualFullName.Should().Contain($"{firstName} {lastName}");
            actualBusinessRole.Should().Be(role);
            actualCategories.Should().Contain(category1);
            actualCategories.Should().Contain(category2);
        }

    }
}
