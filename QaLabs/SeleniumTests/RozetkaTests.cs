using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using FluentAssertions;

namespace SeleniumTests
{
    public class Tests
    {
        [TestFixture]
        public class Chrome_Sample_test
        {
            private IWebDriver driver;
            public string homeURL;

            [SetUp]
            public void SetupTest()
            {
                homeURL = "https://rozetka.com.ua/ua/";
                driver = new ChromeDriver();
            }

            [TearDown]
            public void TearDownTest()
            {
                driver.Close();
            }

            [TestCase("Дакимакура с Рикардо Милосом")]
            [TestCase("javascript для детей")]
            public void SearchAndAddProductToCart(string productQuery)
            {
                driver.Navigate().GoToUrl(homeURL);
                WebDriverWait wait = new WebDriverWait(driver,
                    System.TimeSpan.FromSeconds(15));
                wait.Until(driver =>
                    driver.FindElement(By.ClassName("search-form__submit")));
                var searchBar =
                    driver.FindElement(By.ClassName("search-form__input"));
                searchBar.SendKeys(productQuery);
                var searchButton = driver.FindElement(By.ClassName("search-form__submit"));
                searchButton.Click();
                wait.Until(driver => driver.FindElement(By.ClassName("rz-search-result-qnty")));
                var resultsQuantity = driver.FindElement(By.ClassName("rz-search-result-qnty"));
                resultsQuantity.Text.Should().MatchRegex(@"Знайдено\s?\w*? \d* товар\w*");

                var buyProductButton = driver.FindElements(By.ClassName("goods-tile__buy-button")).First();
                buyProductButton.Click();
                var productName = driver.FindElements(By.ClassName("goods-tile__title")).First().Text;

                driver.FindElement(By.XPath("//button[@opencart]")).Click();
                var productTitle = driver.FindElement(By.ClassName("cart-product__title"));
                productTitle.Text.Should().Contain(productName);
            }
        }
    }
}