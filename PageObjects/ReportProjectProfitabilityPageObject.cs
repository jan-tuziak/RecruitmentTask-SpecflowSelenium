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

        WebDriverWait wait;
        CommonElements ce;

        public ReportProjectProfitabilityPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
            ce = new CommonElements(_webDriver);
        }
        //Finding elements by ID and Css
        private By RunReportButton => By.Name("FilterForm_applyButton");
        private By ReportName => By.CssSelector("h4.form-title");
        private By SampleDataPointLocator => By.CssSelector("tr[data-id]");

        internal void CheckIfCorrectPageIsShown()
        {
            ce.WaitForStatusToDisappear();
            wait.Until(driver => CommonElements.Appears(driver, ReportName));
            _webDriver.FindElement(ReportName).Text.Trim().Should().Be("Project Profitability");
        }

        internal void RunReport()
        {
            wait.Until(driver => CommonElements.Appears(driver, RunReportButton));
            _webDriver.FindElement(RunReportButton).Click();
            ce.WaitForStatusToDisappear();
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
