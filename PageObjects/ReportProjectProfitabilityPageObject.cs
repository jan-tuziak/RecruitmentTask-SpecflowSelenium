using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentTaskSpecflowSelenium.PageObjects
{
    internal class ReportProjectProfitabilityPageObject
    {
        //The Selenium web driver to automate the browser
        private readonly IWebDriver _webDriver;

        //The default wait time in seconds for wait.Until
        public const int DefaultWaitInSeconds = 10;

        WebDriverWait webDriverWait;

        public ReportProjectProfitabilityPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
        }
        //Finding elements by ID and Css
        private IWebElement RunReportButton => _webDriver.FindElement(By.Name("FilterForm_applyButton"));
        private IWebElement ReportName => _webDriver.FindElement(By.CssSelector("h4.form-title"));
        private By SampleDataPointLocator => By.CssSelector("tr[data-id]");

        internal void CheckIfCorrectPageIsShown()
        {
            ReportName.Text.Trim().Should().Be("Project Profitability");
        }

        internal void RunReport()
        {
            Thread.Sleep(1000);
            RunReportButton.Click();
            Thread.Sleep(250);
        }

        internal void CheckIfResultsAreShown()
        {
            bool dataExists;
            try
            {
                dataExists = _webDriver.FindElement(SampleDataPointLocator) != null;
            }
            catch (Exception)
            {
                dataExists = false;
            }
            dataExists.Should().BeTrue();
        }
    }
}
