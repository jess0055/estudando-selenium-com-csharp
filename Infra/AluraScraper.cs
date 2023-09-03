// Implementação do scraper para o site da Alura

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public class AluraScraper : IScraper
{
    private readonly IWebDriver _driver;
    private readonly AluraDbContext _dbContext;

    public AluraScraper(IWebDriver driver, AluraDbContext dbContext)
    {
        _driver = driver;
        _dbContext = dbContext;
    }

    public void ScrapeAndSaveCourses()
    {
        _driver.Navigate().GoToUrl("https://www.alura.com.br/");
        string termoPesquisa = "curso rpa";

        // Realize a busca no site 
        IWebElement searchBox = _driver.FindElement(By.Id("header-barraBusca-form-campoBusca"));
        searchBox.SendKeys(termoPesquisa);
        searchBox.SendKeys(Keys.Enter);

        // Localize o elemento de resultado 
        IWebElement elementoResultado = _driver.FindElement(By.CssSelector(".busca-resultado-nome"));
        // Obtenha o texto do elemento de resultado.
        string textoResultado = elementoResultado.Text.ToLower();
      

        // Verifique se o texto do resultado corresponde ao termo de pesquisa.              
        if (textoResultado.Contains(termoPesquisa))
        {
            Console.WriteLine("O resultado corresponde ao termo de pesquisa.");
       
            IWebElement elementoTitle = _driver.FindElement(By.CssSelector(".busca-resultado-nome"));
            string textoTitle = elementoTitle.Text;

            IWebElement elementoDescription = _driver.FindElement(By.CssSelector(".busca-resultado-descricao"));
            string textoDescription = elementoDescription.Text;

            elementoResultado.Click();

            IWebElement elementoInstructor = _driver.FindElement(By.CssSelector(".instructor-title--name"));
            string textoInstructor = elementoInstructor.Text;

            IWebElement elementoDuration = _driver.FindElement(By.CssSelector(".courseInfo-card-wrapper-infos"));
            string textoDuration = elementoDuration.Text;

            //Console.WriteLine(textoTitle + textoDescription + textoInstructor + textoDuration);
            
            var course = new AluraCourse
            {
                Title = textoTitle,
                Instructor = textoInstructor,
                Description = textoDescription,
                Duration = textoDuration,
            };

            Console.WriteLine(course.Title + course.Description + course.Instructor + course.Duration);
            // Salve o resultado no banco de dados
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();

        }
        else
        {
            Console.WriteLine("O resultado NÃO corresponde ao termo de pesquisa.");
        }

    }
}