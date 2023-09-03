 public class RpaService
    {
        private readonly IScraper _scraper;

        public RpaService(IScraper scraper)
        {
            _scraper = scraper;
        }

        public void Run()
        {
            // Execute a automação
            _scraper.ScrapeAndSaveCourses();
        }
    }