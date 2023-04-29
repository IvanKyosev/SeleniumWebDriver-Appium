using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace SeleniumTests
{
    public class SeleniumTests
    {

        private WebDriver driver;
        private const string baseUrl = "https://contactbookivankyosev.ibk92.repl.co/";

        [SetUp]

        public void OpenWebApp()

        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            driver.Url = baseUrl;
        }

        [TearDown]

        public void CloseWebApp()
        {
            driver.Quit();
        }

        // Navigate to "Contacts" and check for "Steve Jobs"
        [Test]
        public void TestContactsPage()
        {
            var linkContacts = driver.FindElement(By.XPath("//a[contains(.,'Contacts')]"));
            linkContacts.Click();

            var firstName = driver.FindElement(By.XPath("(//td[contains(.,'Steve')])[1]"));
            Assert.That(firstName.Text, Is.EqualTo("Steve"));

            var lastName = driver.FindElement(By.XPath("(//td[contains(.,'Jobs')])[1]"));
            Assert.That(lastName.Text, Is.EqualTo("Jobs"));
        }
        //Navigate to Search Contact and check for Albert Einstein
        [Test]
        public void searchContact() 
        {
            var linkSeacrh = driver.FindElement(By.XPath("(//a[@href='/contacts/search'])[1]"));
            linkSeacrh.Click();

            var inputName = driver.FindElement(By.Id("keyword"));
            inputName.SendKeys("albert");

            var buttonSearch = driver.FindElement(By.Id("search"));
            buttonSearch.Click();

            var firstName = driver.FindElement(By.XPath("(//td[contains(.,'Albert')])[1]"));
            Assert.That(firstName.Text, Is.EqualTo("Albert"));

            var lastName = driver.FindElement(By.XPath("(//td[contains(.,'Einstein')])[1]"));
            Assert.That(lastName.Text, Is.EqualTo("Einstein"));
        }
        //Search for non existing contact
        [Test]  
        public void searchInvalid()
        {
            var linkSeacrh = driver.FindElement(By.XPath("(//a[@href='/contacts/search'])[1]"));
            linkSeacrh.Click();

            var inputName = driver.FindElement(By.Id("keyword"));
            inputName.SendKeys("missing");

            var buttonSearch = driver.FindElement(By.Id("search"));
            buttonSearch.Click();

            Assert.That(driver.PageSource.Contains("No contacts found."));
        }
        //Invalid contact creation
        [Test]          
        public void createInvalid()
        {
            var linkCreate = driver.FindElement(By.XPath("(//a[@href='/contacts/create'])[1]"));
            linkCreate.Click();

            var buttonCreate = driver.FindElement(By.Id("create"));
            buttonCreate.Click();

            var labelErrorMessage = driver.FindElement(By.XPath("//div[@class='err']"));
            Assert.That(labelErrorMessage.Text, Is.EqualTo("Error: First name cannot be empty!"));
        }
        //Create new contact
        [Test]  
        public void createNewContact()
        {
            var linkCreate = driver.FindElement(By.XPath("(//a[@href='/contacts/create'])[1]"));
            linkCreate.Click();

            var inputFirstName = driver.FindElement(By.Id("firstName"));
            inputFirstName.SendKeys("Ivan");

            var inputLastName = driver.FindElement(By.Id("lastName"));
            inputLastName.SendKeys("Kyosev");

            var inputEmail = driver.FindElement(By.Id("email"));
            inputEmail.SendKeys("ivanbkyosev@gmail.com");

            var buttonCreate = driver.FindElement(By.Id("create"));
            buttonCreate.Click();

            var firstName = driver.FindElement(By.XPath("//td[contains(.,'Ivan')]"));
            Assert.That(firstName.Text, Is.EqualTo("Ivan"));

            var lastName = driver.FindElement(By.XPath("//td[contains(.,'Kyosev')]"));
            Assert.That(lastName.Text, Is.EqualTo("Kyosev"));

            var email = driver.FindElement(By.XPath("//td[contains(.,'ivanbkyosev@gmail.com')]"));
            Assert.That(email.Text, Is.EqualTo("ivanbkyosev@gmail.com"));
        }
    }
}