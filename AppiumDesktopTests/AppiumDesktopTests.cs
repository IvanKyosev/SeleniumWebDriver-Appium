using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using NUnit.Framework.Constraints;
using OpenQA.Selenium.Internal;

namespace AppiumDesktopTests
{
    public class AppiumDesktopTests
    {
        private WindowsDriver<WindowsElement> driver;
        private AppiumOptions options;

        private const string appLocation = @"C:\DemoApps\ContactBook-DesktopClient\ContactBook-DesktopClient.exe";
        private const string appiumServer = "http://127.0.0.1:4723/wd/hub";
        private const string appServer = "https://contactbookivankyosev.ibk92.repl.co/api";

        [SetUp]
        public void PrepareApp()
        {
            this.options = new AppiumOptions();
            options.AddAdditionalCapability("app", appLocation);
            driver = new WindowsDriver<WindowsElement>(new Uri(appiumServer), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
        [TearDown]
        public void CloseApp()
        {
            driver.Quit();
        }
        //Search for Steve Jobs
        [Test]
        public void Test_Search()
        {
            var inputAppUrl = driver.FindElementByAccessibilityId("textBoxApiUrl");
            inputAppUrl.Clear();
            inputAppUrl.SendKeys(appServer);

            // press Connect button
            var buttonConnect = driver.FindElementByAccessibilityId("buttonConnect");
            buttonConnect.Click();

            driver.SwitchTo().Window(driver.WindowHandles.Last());

            // fill the search bar
            var inputKeyword = driver.FindElementByAccessibilityId("textBoxSearch");
            inputKeyword.SendKeys("steve");

            // press button Search  
            var buttonSearch = driver.FindElementByAccessibilityId("buttonSearch");
            buttonSearch.Click();

             var firstName = driver.FindElementByAccessibilityId("FirstName");
             Assert.That(firstName.Text, Is.EqualTo("Steve"));

             var lastName = driver.FindElementByAccessibilityId("LastName");
             Assert.That(lastName.Text, Is.EqualTo("Jobs"));
        }
    }
}