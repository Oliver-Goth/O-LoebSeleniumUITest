using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

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

            // Get all posts and ensure there are more than 0
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            ReadOnlyCollection<IWebElement> listOfPosts = wait.Until(p => p.FindElements(By.CssSelector("div[class*='mb-2 p-1']")));
            Assert.IsTrue(listOfPosts.Count() > 0);

            // Pick first post and click it
            IWebElement firstPost = listOfPosts.First();
            Assert.IsNotNull(firstPost);
            firstPost.Click();

            // Check card has been filled with information
            IWebElement greenCard = driver.FindElement(By.ClassName("card"));
            Assert.IsNotNull(greenCard);
            ReadOnlyCollection<IWebElement> h4Elements = greenCard.FindElements(By.TagName("H4"));
            Assert.IsTrue(h4Elements.Count == 2);

            // Check h4 has values
            IWebElement firstH4 = h4Elements.First();
            Assert.IsNotNull(firstH4);
            Assert.IsNotNull(firstH4.Text);
            IWebElement secondH4 = h4Elements[1];
            Assert.IsNotNull(secondH4);
            Assert.IsNotNull(secondH4.Text.Split(":")[1]);

            // Check span has values
            ReadOnlyCollection<IWebElement> cardSpans = greenCard.FindElements(By.TagName("span"));
            Assert.IsTrue(cardSpans.Count() == 2);
            IWebElement firstSpan = cardSpans.First();
            Assert.IsNotNull(firstSpan);
            Assert.IsNotNull(firstSpan.Text.Split(":")[1]);
            IWebElement secondSpan = cardSpans[1];
            Assert.IsNotNull(secondSpan);
            Assert.IsNotNull(secondSpan.Text.Split(":")[1]);
        }
    }
}
