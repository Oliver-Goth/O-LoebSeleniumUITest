using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace O_LoebSeleniumUITest
{
    [TestClass]
    public class SeleniumU5
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
            driver.Navigate().GoToUrl(Constants.Url + "quizquestions.html");
        }

        [TestMethod]
        public void AddQuestionToQuiz() 
        {
            Assert.AreEqual("O-løb", driver.Title);
        }
    }
}
