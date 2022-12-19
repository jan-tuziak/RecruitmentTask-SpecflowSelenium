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

        CommonElements commonElements;

        WebDriverWait wait;

        public ReportsPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
            commonElements = new CommonElements(_webDriver);
        }
        //Finding elements by ID and Css
        private IWebElement FilterTextInput => _webDriver.FindElement(By.Name("filter_text"));
        //private IWebElement FilterTextProjectProfitabilityOption => _webDriver.FindElement(By.CssSelector("div.option-cell.input-label"));
        private By FirstResultLinkLocator => By.CssSelector("table.listView>tbody>tr:first-child>td:nth-child(3)>span>a");
        private IWebElement FirstResultLink => _webDriver.FindElement(FirstResultLinkLocator);

        internal void GoToReport(string reportName)
        {
            try
            {
                FilterTextInput.Clear();
            }
            catch (StaleElementReferenceException ex)
            {
                FilterTextInput.Clear();
            }
            FilterTextInput.SendKeys(reportName);
            Thread.Sleep(250);
            FilterTextInput.SendKeys(Keys.Enter);
            Thread.Sleep(250);
            try
            {
                FirstResultLink.Click();
            }
            catch(StaleElementReferenceException ex) 
            {
                FirstResultLink.Click();
            } 

        }
    }
}
