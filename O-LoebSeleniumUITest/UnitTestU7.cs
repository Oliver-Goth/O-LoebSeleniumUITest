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

            // Static navigation to our site
            driver.Navigate().GoToUrl("indsæt url");
        }

        // Cleans up the driver after it is initialized
        [ClassCleanup]
        public static void Cleanup()
        {
            driver.Dispose();
        }

        [TestMethod]
        public void TestForRunTypeCanBeSelected()
        {

            // Selecting the dropdown menu
            IWebElement dropDown = driver.FindElement(By.Id("run-types"));

            SelectElement select = new SelectElement(dropDown);

            var options = select.Options;

            // Checks for the 2 options contains o-løb and stjerneløb in the text and in the correct order

            Assert.AreEqual("o-løb", options[0].Text);
            Assert.AreEqual("stjerneløb", options[1].Text);

        }

        public void TestForRunCreated()
        {

            // Selecting create button
            IWebElement createRunButton = driver.FindElement(By.Id("create-run-button"));


            // Selecting dropdown menu
            IWebElement dropDown = driver.FindElement(By.Id("run-types"));

            SelectElement select = new SelectElement(dropDown);

            var options = select.SelectedOption;


            // Checks if the selcted option in the dropdown menu contains o-løb or stjerneløb

            if (options.Text == "o-løb")
            {
                createRunButton.Click();

                driver.Navigate().GoToUrl("indsæt url/createoloeb");

                string expectedUrl = "indsæt url/createoloeb";

                Assert.AreEqual(expectedUrl, driver.Url);

            }
            else if (options.Text == "stjerneløb")
            {

                createRunButton.Click();

                driver.Navigate().GoToUrl("indsæt url/createstjerneloeb");

                string expectedUrl = "indsæt url/createstjerneloeb";

                Assert.AreEqual(expectedUrl, driver.Url);

            }


        }
    }
}