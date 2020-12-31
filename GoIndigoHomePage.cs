using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GoIbiboSpecflow
{
    class GoIndigoHomePage
    {
        private string _url;
        public IWebDriver Driver { get;}
        public WebDriverWait Wait { get; }
        public IJavaScriptExecutor JS { get; }
        public GoIndigoHomePage()
        {
            _url = "https://www.goindigo.in/";
            Driver = new ChromeDriver();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Manage().Window.Maximize();
            Driver.Manage().Cookies.DeleteAllCookies();
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            JS = (IJavaScriptExecutor) Driver;
        }
        public GoIndigoHomePage Open()
        {
            Driver.Navigate().GoToUrl(_url);
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='herocarousel section']")));
            return this;
        }
        public GoIndigoHomePage AddPassengers(int adults=1, int children=0, int infants=0)
        {
            Driver.FindElement(By.XPath("//div[@class='ig-input-group field-float']/input[@class='form-control hpBookingForm passengerInputField pax-class-count']")).Click();
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='passenger-dropdown pax-selection-row']")));
            if(adults>1)
            {
                IWebElement AdultPlus = Driver.FindElement(By.XPath("//div[@class='passenger-dropdown pax-selection-row']//li[@class='adult-pax-list extra-seat']//button[@title='Up']"));
                //IWebElement AdultMinus = Driver.FindElement(By.XPath("//div[@class='passenger-dropdown pax-selection-row']//li[@class='adult-pax-list extra-seat']//span[@class='icon-minus']"));
                int getCount; 
                var currentAdults = 1;
                while(currentAdults<adults)
                {

                    JS.ExecuteScript("arguments[0].click();", AdultPlus);
                    //Wait.Until(ExpectedConditions.ElementToBeClickable(AdultPlus)).Click();
                    getCount = int.Parse(Wait.Until(d => d.FindElement(By.XPath("//div[@class='passenger-dropdown pax-selection-row']//li[@class='adult-pax-list extra-seat']//input[@class='counter adult-pax']")).GetAttribute("value")));
                    if (getCount == currentAdults + 1)
                        currentAdults++;
                }
            }

            if(children>0)
            {
                IWebElement ChildPlus = Driver.FindElement(By.XPath("//div[@class='passenger-dropdown pax-selection-row']//li[@class='child-pax-list extra-seat']//span[@class='icon-plus']"));
                //IWebElement AdultMinus = Driver.FindElement(By.XPath("//div[@class='passenger-dropdown pax-selection-row']//li[@class='adult-pax-list extra-seat']//span[@class='icon-minus']"));
                int getCount;
                var currentChild = 0;
                while (currentChild < children)
                {

                    JS.ExecuteScript("arguments[0].click();", ChildPlus);
                    //ChildPlus.Click();
                    getCount = int.Parse(Wait.Until(d => d.FindElement(By.XPath("//div[@class='passenger-dropdown pax-selection-row']//li[@class='child-pax-list extra-seat']//input[@class='counter child-pax']")).GetAttribute("value")));
                    if (getCount == currentChild + 1)
                        currentChild++;
                }
            }
            
            if(infants>0)
            {
                IWebElement InfantPlus = Driver.FindElement(By.XPath("//div[@class='passenger-dropdown pax-selection-row']//li[@class='infant-pax-list']//span[@class='icon-plus']"));
                //IWebElement AdultMinus = Driver.FindElement(By.XPath("//div[@class='passenger-dropdown pax-selection-row']//li[@class='adult-pax-list extra-seat']//span[@class='icon-minus']"));
                int getCount;
                var currentInfant = 0;
                while (currentInfant < infants)
                {

                    JS.ExecuteScript("arguments[0].click();", InfantPlus);
                    //InfantPlus.Click();
                    getCount = int.Parse(Wait.Until(d => d.FindElement(By.XPath("//div[@class='passenger-dropdown pax-selection-row']//li[@class='infant-pax-list']//input[@class='counter infant-pax']")).GetAttribute("value")));
                    if (getCount == currentInfant + 1)
                        currentInfant++;
                }
            }
            return this;
        }

        public GoIndigoHomePage AddSource(string source)
        {
            var from = Driver.FindElement(By.ClassName("or-src-city"));
            //from.Clear();
            JS.ExecuteScript("arguments[0].value = '';", from);
            from.SendKeys(source);
            //Thread.Sleep(100);
            //var list = Driver.FindElements(By.XPath("//*[@class='autocomplete-result station-result clearfix airport-item pop-dest-stn']"));
            //JS.ExecuteScript("arguments[0].click();", list[0]);
            from.SendKeys(Keys.Return);
            Thread.Sleep(500);
            //from.Submit();
            return this;
        }

        public GoIndigoHomePage AddDestination(string destination)
        {
            var to = Driver.FindElement(By.ClassName("or-dest-city"));
            JS.ExecuteScript("arguments[0].value='';", to);
            to.SendKeys(destination);
            Thread.Sleep(100);
            //var list = Driver.FindElements(By.XPath("//*[@class='autocomplete-result station-result clearfix airport-item pop-dest-stn']"));
            //JS.ExecuteScript("arguments[0].click();", list[0]);
            to.SendKeys(Keys.Return);
            Thread.Sleep(500);
            //to.Submit();
            return this;
        }

        public GoIndigoHomePage SetDepartureDate(string date)
        {
            //var dateSelector=Driver.FindElement(By.XPath("//input[@name='or-depart'][@class='form-control or-depart igInitCalendar']"));
            //JS.ExecuteScript("arguments[0].click();", dateSelector);
            //Thread.Sleep(100);
            var next = Driver.FindElement(By.XPath("//a[@data-handler='next']"));
            var prev = Driver.FindElement(By.XPath("//a[@data-handler='prev']"));
            var Date = DateTime.Parse(date);
            
            string CalendarYear;
            string CalendarMonth;
            //Set Year
            do
            {
                CalendarYear = Driver.FindElement(By.XPath("//div[@class='ui-datepicker-group ui-datepicker-group-first']//span[@class='ui-datepicker-year']")).Text;
                if (CalendarYear.Contains(Date.Year.ToString()))
                    break;
                JS.ExecuteScript("arguments[0].click();", next);
                Thread.Sleep(200);
                next = Driver.FindElement(By.XPath("//a[@data-handler='next']"));
            } while (!CalendarYear.Contains(Date.Year.ToString()));

            do
            {
                CalendarMonth = Driver.FindElement(By.XPath("//div[@class='ui-datepicker-group ui-datepicker-group-first']//span[@class='ui-datepicker-month']")).Text;
                if (CalendarMonth.Contains(Date.ToString("MMMM")))
                    break;
                JS.ExecuteScript("arguments[0].click();", next);
                Thread.Sleep(200);
                next = Driver.FindElement(By.XPath("//a[@data-handler='next']"));
            } while (!CalendarMonth.Contains(Date.ToString("MMMM")));

            var days = Driver.FindElements(By.XPath("//div[@class='ui-datepicker-group ui-datepicker-group-first']//tr/td[@data-handler='selectDay']"));
            JS.ExecuteScript("arguments[0].click();", days[Date.Day - 1]);

            var doneBtn = Driver.FindElement(By.XPath("//a[@class='btn btn-primary dateClose']"));
            JS.ExecuteScript("arguments[0].click();", doneBtn);
            return this;
        }

        public void SearchFlights()
        {
            var searchBtn = Driver.FindElement(By.XPath("//span[@class='hp-src-btn']"));
            JS.ExecuteScript("arguments[0].click();", searchBtn);
        }
    }
}
