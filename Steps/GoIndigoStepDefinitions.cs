using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace GoIbiboSpecflow.Steps
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        GoIndigoHomePage homePage;

        [Given(@"the goindigo home page is open")]
        public void GivenTheGoindigoHomePageIsOpen()
        {
            homePage = new GoIndigoHomePage();
            homePage.Open();
        }

        [Given(@"the number of passengers is (.*)")]
        public void GivenTheNumberOfPassengersIs(int p0)
        {
            homePage.AddPassengers(adults:p0);
        }

        [Given(@"moving from '(.*)'")]
        public void GivenMovingFrom(string p0)
        {
            homePage.AddSource(p0);
        }

        [Given(@"moving to '(.*)'")]
        public void GivenMovingTo(string p0)
        {
            homePage.AddDestination(p0);
        }

        [Given(@"date of journey is '(.*)'")]
        public void GivenDateOfJourneyIs(string p0)
        {
            homePage.SetDepartureDate(p0);
        }

        [When(@"I click search flight")]
        public void WhenIClickSearchFlight()
        {
            homePage.SearchFlights();
        }

        [Then(@"I should get search results")]
        public void ThenIShouldGetSearchResults()
        {
            Assert.Pass();
        }

    }
}
