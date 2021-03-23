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
            var result = homepage.GetAllTasks();
            Assert.That(result.Select(t => t.Text).Contains(taskName));
        }

        [TestCase("Task")]
        public void RemoveTodo(string taskName)
        {
            homepage.GoToPage();
            homepage.AddTask(taskName);
            homepage.DeleteTask(taskName);
            var result = homepage.GetAllTasks();
            Assert.That(!result.Any());
        }
    }
}
