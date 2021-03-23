using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumTests.Pages
{
    class Homepage
    {
        private readonly string homeUrl = "https://todomvc.com/examples/angularjs";

        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private IWebElement TodoInput => wait.Until(driver => driver.FindElement(By.ClassName("new-todo")));

        public Homepage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        public void GoToPage()
        {
            driver.Navigate().GoToUrl(homeUrl);
        }

        public void AddTask(string taskName)
        {
            TodoInput.SendKeys(taskName);
            TodoInput.SendKeys(Keys.Enter);
        }

        public void DeleteTask(string taskName)
        {
            var task = GetAllTasks().FirstOrDefault(t => t.Text == taskName);
            if (task != null)
            {
                //IWebElement taskContainer = driver.FindElement(By.XPath($"//div[.//label[contains(text(),'{taskName}')]]"));
                IWebElement deleteButton = driver.FindElement(By.XPath($"//div[.//label[contains(text(),'{taskName}')]]//button"));
                Actions action = new Actions(driver);
                action.MoveToElement(deleteButton);
                deleteButton.Click();
            }
            else throw new ArgumentNullException(taskName);
        }

        public void ChangeTaskStatus()
        {

        }

        public IEnumerable<IWebElement> GetAllTasks()
        {
            return driver.FindElements(By.ClassName("ng-binding"));
        }
    }
}
