using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace O_LoebSeleniumUITest
{
    [TestClass]
    public class UnitTestU7
    {

        private static readonly string DriverDir = "C:\\Webdrivers";
        private static IWebDriver driver;

        // initialized the webdriver
        [ClassInitialize]

        public static void Setup(TestContext testContext)
        {
            driver = new ChromeDriver(DriverDir);
        }

        // Cleans up the driver after it is initialized
        [ClassCleanup]
        public static void Cleanup()
        {
            driver.Dispose();
        }

        // Runs before each test
        [TestInitialize] 
        public void Setup() 
        {
            // Always goes back to the index before running tests
            driver.Navigate().GoToUrl(Constants.Url);
        }

        [TestMethod]
        public void TestForRunTypeCanBeSelected()
        {
            Assert.AreEqual("O-l�b", driver.Title);

            // Selecting the dropdown menu
            IWebElement dropDown = driver.FindElement(By.ClassName("dropdown"));

            SelectElement select = new SelectElement(dropDown);

            var options = select.Options;

            // Checks for the 2 options contains o-l�b and stjernel�b in the text and in the correct order

            Assert.AreEqual("O-l�b", options[0].Text);
            Assert.AreEqual("Stjerne-l�b", options[1].Text);

        }

        [TestMethod]
        public void TestForRunCreatedOLoeb()
        {
            // Selecting create button
            IWebElement createRunButton = driver.FindElement(By.ClassName("btn-success"));

            IWebElement runNameInput = driver.FindElement(By.ClassName("rounded"));

            runNameInput.SendKeys("Selenium Test O-l�b");

            Assert.AreEqual("Selenium Test O-l�b", runNameInput.GetAttribute("value"));

            // Selecting dropdown menu
            IWebElement dropDown = driver.FindElement(By.ClassName("dropdown"));

            SelectElement select = new SelectElement(dropDown);

            select.SelectByIndex(0);

            var option = select.SelectedOption;

            Assert.AreEqual("O-l�b", option.Text);

            createRunButton.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IAlert alert = wait.Until(a => a.SwitchTo().Alert());

            Assert.AreEqual("Run was added", alert.Text);

            alert.Accept();

            wait.Until(u => u.Url.Contains("post.html"));

            Assert.AreEqual("O-l�b", driver.Title);
        }

        [TestMethod]
        public void TestForRunCreatedStjerneLoeb()
        {
            // Selecting create button
            IWebElement createRunButton = driver.FindElement(By.ClassName("btn-success"));

            IWebElement runNameInput = driver.FindElement(By.ClassName("rounded"));

            runNameInput.SendKeys("Selenium Test Stjerne-l�b");

            Assert.AreEqual("Selenium Test Stjerne-l�b", runNameInput.GetAttribute("value"));

            // Selecting dropdown menu
            IWebElement dropDown = driver.FindElement(By.ClassName("dropdown"));

            SelectElement select = new SelectElement(dropDown);

            select.SelectByIndex(1);

            var option = select.SelectedOption;

            Assert.AreEqual("Stjerne-l�b", option.Text);

            createRunButton.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IAlert alert = wait.Until(a => a.SwitchTo().Alert());

            Assert.AreEqual("Run was added", alert.Text);

            alert.Accept();

            wait.Until(u => u.Url.Contains("post.html"));

            Assert.AreEqual("O-l�b", driver.Title);
        }
    }
}