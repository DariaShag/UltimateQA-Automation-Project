using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using UltimateQAInfra;

namespace Tests
{
    public class SetUp
    {
        public static IWebDriver driver;
        protected static IWebElement? _mainWindow;
        protected static PageContainer _ultimate;

        public SetUp()
        {

        }

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new EdgeDriver();
            driver.Navigate().GoToUrl("https://ultimateqa.com/complicated-page");
            driver.Manage().Window.Maximize();
            Thread.Sleep(5000);
            _mainWindow = driver.FindElement(By.Id("page-container"));
            _ultimate = new PageContainer(_mainWindow);

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Dispose();
            driver.Quit();
        }


    }
}