using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using NuGet.Frameworks;

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

            // Gets run list and checks if it has elements
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            ReadOnlyCollection<IWebElement> listOfRuns = wait.Until(run => run.FindElements(By.ClassName("bg-primary")));
            Assert.IsTrue(listOfRuns.Count > 0);

            // Chooses first run in list and clicks it
            IWebElement? firstRun = listOfRuns.FirstOrDefault();
            Assert.IsNotNull(firstRun);
            firstRun.Click();

            // Checks if the name of the run is not null
            IWebElement chosenRun = driver.FindElement(By.ClassName("px-4"));
            IWebElement secondH5 = chosenRun.FindElements(By.TagName("h5"))[1];
            Assert.IsNotNull(secondH5);
            Assert.IsNotNull(secondH5.Text);

            // Checks if the runtype is of the known type
            IWebElement secondH6 = chosenRun.FindElements(By.TagName("h6"))[1];
            Assert.IsTrue(secondH6.Text == "O-løb" || secondH6.Text == "Stjerne-løb");

            // Find input elements to test create post
            ReadOnlyCollection<IWebElement> postInputs = driver.FindElements(By.CssSelector("input[class=mb-3]"));
            Assert.IsTrue(postInputs.Count == 2);
            
            // Finds name element, writes in it and checks
            IWebElement nameInput = postInputs[0];
            Assert.IsNotNull(nameInput);
            nameInput.SendKeys("Post Selenium test");
            Assert.AreEqual("Post Selenium test", nameInput.GetAttribute("value"));

            // Finds radius element, writes in it and checks
            IWebElement radiusInput = postInputs[1];
            Assert.IsNotNull(radiusInput);
            // Had to invoke clear, otherwise there would always be a leading zero
            radiusInput.Clear();
            radiusInput.SendKeys("100");
            Assert.AreEqual("100", radiusInput.GetAttribute("value"));

            // Find latitude/longitude inputs
            ReadOnlyCollection<IWebElement> tudeInputs = driver.FindElements(By.CssSelector("input[class*='mt-2 w-50']"));
            Assert.AreEqual(2,tudeInputs.Count);

            // Test latitude input field
            IWebElement latitudeInput = tudeInputs[0];
            Assert.IsNotNull(latitudeInput);
            latitudeInput.Clear();
            latitudeInput.SendKeys("55");
            Assert.AreEqual("55",latitudeInput.GetAttribute("value"));

            // Test longitude input field
            IWebElement longitudeInput = tudeInputs[1];
            Assert.IsNotNull(longitudeInput);
            longitudeInput.Clear();
            longitudeInput.SendKeys("12");
            Assert.AreEqual("12",longitudeInput.GetAttribute("value"));

            // Find button and click
            WebDriverWait secondWait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            IWebElement createPostButton = secondWait.Until(run => run.FindElement(By.CssSelector("button[class*='btn btn-primary p-1 fs-5 mt-2']")));
            Assert.IsNotNull(createPostButton);
            // Seleniun can't click elements outside it's window view. Force scrolling to the button 
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", createPostButton);
            // Need to force a sleep for the element to be clickable
            Thread.Sleep(1000);
            createPostButton.Click();

            // Check if post has been added to list
            ReadOnlyCollection<IWebElement> postAdded = secondWait.Until(p => p.FindElements(By.CssSelector("div[class*='w-100 max-h']")));
            Assert.IsNotNull(postAdded);
            Assert.IsTrue(postAdded.Count > 0);

            // Find add post to run button and click
            IWebElement addPostsButton = secondWait.Until(a => a.FindElement(By.ClassName("btn-success")));
            Assert.IsNotNull(addPostsButton);
            addPostsButton.Click();

            // Handle alert box
            IAlert alert = wait.Until(a => a.SwitchTo().Alert());
            Assert.AreEqual("Posterne blev tilføjet", alert.Text);
        }
    }
}
