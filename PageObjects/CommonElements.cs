using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentTaskSpecflowSelenium.PageObjects
{
    /// <summary>
    /// UI Elements shared across pages
    /// </summary>
    public class CommonElements
    {
        //The Selenium web driver to automate the browser
        private readonly IWebDriver _webDriver;
        private readonly WebDriverWait wait;

        public CommonElements(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(15));
        }

        private By StatusTextLocator => By.Id("AjaxStatusDiv");
        private IWebElement StatusText => _webDriver.FindElement(StatusTextLocator);

        public void WaitForStatusToDisappear()
        {
            //Wait until Status Text is invisible
            Thread.Sleep(1);
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(StatusTextLocator));
        }
    }
}
