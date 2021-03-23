using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.Pages;
using System.Linq;

namespace SeleniumTests
{
    [TestFixture]
    class TodomvcTests
    {
        private IWebDriver driver;
        private Homepage homepage;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            homepage = new Homepage(driver);
        }

        [TearDown]
        public void TearDownTest()
        {
            driver.Close();
        }

        [TestCase("Task")]
        [TestCase("Another task")]
        [TestCase("And another task")]
        public void AddTodo(string taskName)
        {
            homepage.GoToPage();
            homepage.AddTask(taskName);
            var tasks = homepage.GetAllTasks();
            Assert.That(tasks.Select(t => t.Text).Contains(taskName));
        }

        [TestCase("Task")]
        [TestCase("Another task")]
        public void RemoveTodo(string taskName)
        {
            homepage.GoToPage();
            homepage.AddTask(taskName);
            homepage.DeleteTask(taskName);
            var tasks = homepage.GetAllTasks();
            Assert.That(!tasks.Any());
        }
        
        [TestCase("Some task")]
        public void ChangeTodoStatus(string taskName)
        {
            homepage.GoToPage();
            homepage.AddTask(taskName);
            homepage.ChangeTaskStatus(taskName);
            homepage.SwitchTab(Homepage.Tabs.Completed);
            var tasks = homepage.GetAllTasks();
            Assert.That(tasks.Any(t => t.Text.Contains(taskName)));
        }
    }
}
