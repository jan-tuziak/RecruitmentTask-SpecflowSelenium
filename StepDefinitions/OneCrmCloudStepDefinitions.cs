using RecruitmentTaskSpecflowSelenium.Drivers;
using RecruitmentTaskSpecflowSelenium.PageObjects;
using System;
using TechTalk.SpecFlow;

namespace RecruitmentTaskSpecflowSelenium.StepDefinitions
{
    [Binding]
    public class OneCrmCloudStepDefinitions
    {
        private readonly LoginPageObject _loginPageObject;
        private readonly HomePageObject _homePageObject;

        public OneCrmCloudStepDefinitions(BrowserDriver browserDriver)
        {
            _loginPageObject = new LoginPageObject(browserDriver.Current);
            _homePageObject = new HomePageObject(browserDriver.Current);
        }

        [Then(@"I logout")]
        public void ThenILogout()
        {
            _homePageObject.Logout();
        }

        [Given(@"I login")]
        public void GivenILogin()
        {
            //Delegate to Page Object
            _loginPageObject.LoginAsAdmin();
        }

        [Given(@"I navigate to '([^']*)'")]
        public void GivenINavigateTo(string tabName)
        {
            _homePageObject.NavigateTo(tabName);
        }

        [When(@"I create a new contact '([^']*)' '([^']*)' '([^']*)' '([^']*)' '([^']*)'")]
        public void WhenICreateANewContact(string firstName, string lastname, string role, string category1, string category2)
        {
            throw new PendingStepException();
        }

        [Then(@"The new contact is '([^']*)' '([^']*)' '([^']*)' '([^']*)' '([^']*)'")]
        public void ThenTheNewContactIs(string firstName, string lastname, string role, string category1, string category2)
        {
            throw new PendingStepException();
        }

        [Given(@"I click create new contact")]
        public void GivenIClickCreateNewContact()
        {
            throw new PendingStepException();
        }

    }
}
