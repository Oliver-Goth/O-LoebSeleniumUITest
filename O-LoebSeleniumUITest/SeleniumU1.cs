using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;

namespace O_LoebSeleniumUITest
{
    [TestClass]
    public class SeleniumU1
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
            // Always goes back to the post before running tests
            driver.Navigate().GoToUrl(Constants.Url + "post.html");
        }

        [TestMethod]
        public void TestAddPostToRun()
        {
            Assert.AreEqual("O-løb", driver.Title);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            ReadOnlyCollection<IWebElement> listOfRuns = wait.Until(run => run.FindElements(By.ClassName("bg-primary")));

            Assert.IsTrue(listOfRuns.Count > 0);

            IWebElement? firstRun = listOfRuns.FirstOrDefault();

            Assert.IsNotNull(firstRun);

            firstRun.Click();

            IWebElement chosenRun = driver.FindElement(By.ClassName("px-4"));

            IWebElement secondH5 = chosenRun.FindElements(By.TagName("h5"))[1];

            Assert.IsNotNull(secondH5);

            Assert.IsNotNull(secondH5.Text);

            IWebElement secondH6 = chosenRun.FindElements(By.TagName("h6"))[1];

            Assert.IsTrue(secondH6.Text == "O-løb" || secondH6.Text == "Stjerne-løb");
        }
    }
}
