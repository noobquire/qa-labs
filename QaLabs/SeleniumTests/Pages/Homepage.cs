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
                IWebElement deleteButton = driver.FindElement(By.XPath($"//div[.//label[contains(text(),'{taskName}')]]//button"));

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].click();", deleteButton);
            }
            else throw new ArgumentNullException(taskName);
        }

        public void ChangeTaskStatus(string taskName)
        {
            IWebElement taskStatusButton = driver.FindElement(By.XPath($"//div[.//label[contains(text(),'{taskName}')]]//input[@ng-change='toggleCompleted(todo)']"));
            taskStatusButton.Click();
        }

        public void SwitchTab(Tabs tab)
        {
            IWebElement tabElement = tab switch
            {
                Tabs.All => driver.FindElement(By.XPath("//a[contains(text(), 'All')]")),
                Tabs.Active => driver.FindElement(By.XPath("//a[contains(text(), 'Active')]")),
                Tabs.Completed => driver.FindElement(By.XPath("//a[contains(text(), 'Completed')]")),
            };
        }

        public IEnumerable<IWebElement> GetAllTasks()
        {
            return driver.FindElements(By.XPath(@"//label[contains(@class, 'ng-binding')]"));
        }

        public enum Tabs
        {
            All, Active, Completed
        }
    }
}
