﻿using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using NuGet.Frameworks;

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
            driver.Manage().Window.Maximize();
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
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

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

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Check random question
            IWebElement randomQuestionButton = wait.Until(q => q.FindElement(By.ClassName("btn-primary")));
            Assert.IsNotNull(randomQuestionButton);
            // Need to force the element into view before it can be clicked
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", randomQuestionButton);
            // Need to force a sleep for the element to be clickable
            Thread.Sleep(1000);
            randomQuestionButton.Click();
            IWebElement questionTextarea = driver.FindElement(By.CssSelector("textarea[class=w-100]"));
            Assert.IsNotNull(questionTextarea);
            Assert.IsTrue(string.IsNullOrEmpty(questionTextarea.Text));

            // Write answers
            ReadOnlyCollection<IWebElement> answerInputs = driver.FindElements(By.CssSelector("input[class=w-75]"));
            Assert.IsTrue(answerInputs.Count() == 4);
            for(int i = 0; i < answerInputs.Count(); i++)
            {
                answerInputs[i].SendKeys("Selenium svar " + i);
            }

            IWebElement addQuestionButton = wait.Until(q => q.FindElement(By.ClassName("btn-success")));
            Assert.IsNotNull(addQuestionButton);
            addQuestionButton.Click();

            // Handle alert box
            IAlert alert = wait.Until(a => a.SwitchTo().Alert());
            Assert.AreEqual("Spørgsmål og svar tilføjet", alert.Text);
            alert.Accept();
        }
    }
}
