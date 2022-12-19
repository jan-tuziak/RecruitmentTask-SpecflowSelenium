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
        private readonly ContactsPageObject _contactsPageObject;
        private readonly ContactsCreateRecordPageObject _contactsCreateRecordPageObject;
        private readonly ContactsDetailPageObject _contactsDetailPageObject;

        public OneCrmCloudStepDefinitions(BrowserDriver browserDriver)
        {
            _loginPageObject = new LoginPageObject(browserDriver.Current);
            _homePageObject = new HomePageObject(browserDriver.Current);
            _contactsPageObject =  new ContactsPageObject(browserDriver.Current);
            _contactsCreateRecordPageObject = new ContactsCreateRecordPageObject(browserDriver.Current);
            _contactsDetailPageObject = new ContactsDetailPageObject(browserDriver.Current);
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
        public void WhenICreateANewContact(string firstName, string lastName, string role, string category1, string category2)
        {
            _contactsCreateRecordPageObject.CreateNewContact(firstName, lastName, role, category1, category2);
        }

        [Then(@"The new contact is '([^']*)' '([^']*)' '([^']*)' '([^']*)' '([^']*)'")]
        public void ThenTheNewContactIs(string firstName, string lastName, string role, string category1, string category2)
        {
            _contactsDetailPageObject.CheckNewContact(firstName, lastName, role, category1, category2);
        }

        [Given(@"I click create new contact")]
        public void GivenIClickCreateNewContact()
        {
            _contactsPageObject.CreateContact();
        }

    }
}
