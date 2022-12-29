using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;

namespace RecruitmentTaskSpecflowSelenium.PageObjects
{
    internal class ReportsPageObject
    {
        //The Selenium web driver to automate the browser
        private readonly IWebDriver _webDriver;

        //The default wait time in seconds for wait.Until
        public const int DefaultWaitInSeconds = 10;

        CommonElements ce;

        WebDriverWait wait;

        public ReportsPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
            ce = new CommonElements(_webDriver);
        }
        //Finding elements by ID and Css
        private By FilterTextInput => By.Name("filter_text");
        private By FirstResultLink => By.CssSelector("table.listView>tbody>tr:first-child>td:nth-child(3)>span>a");

        internal void GoToReport(string reportName)
        {
            wait.Until(driver => CommonElements.Appears(driver, FilterTextInput));
            _webDriver.FindElement(FilterTextInput).Clear();

            _webDriver.FindElement(FilterTextInput).SendKeys(reportName);

            ce.WaitForStatusToDisappear();
            
            _webDriver.FindElement(FilterTextInput).SendKeys(Keys.Enter);
            ce.WaitForStatusToDisappear();
 
            wait.Until(driver => CommonElements.Appears(driver, FirstResultLink));
            _webDriver.FindElement(FirstResultLink).Click();
        }
    }
}
