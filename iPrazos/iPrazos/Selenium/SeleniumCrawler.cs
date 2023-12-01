using Crawler.Events;
using iPrazos.Events;
using iPrazos.Exceptions;
using IPrazos.Entity;
using MediatR;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace iPrazos.Selenium
{
	public class SeleniumCrawler
	{
		public List<ProxyConnection> ProxyList = new List<ProxyConnection>();

		private string HtmlFolderName = "HTML";
		private string FilePathJSON = "proxyList.json";
		// Chrome Config
		private ChromeOptions ChromeOptions;

		private Dictionary<string, List<ProxyConnection>> PageData = new Dictionary<string, List<ProxyConnection>>();

		private bool NextPageExists = true;

		private readonly IMediator _mediator;

		private int ActualPage = 0;
		public SeleniumCrawler(IMediator mediator)
		{
			ChromeOptions = new ChromeOptions();
			ChromeOptions.AddArguments("--ignore-certificate-errors");
			_mediator = mediator;
		}

		public void Init(int startPage)
		{
			ActualPage = startPage;
			Console.WriteLine($"Start Thread: {Thread.CurrentThread.Name} na {ActualPage}");
			// HTML FOLDER
			if (!Directory.Exists(HtmlFolderName))
			{
				Directory.CreateDirectory(HtmlFolderName);
			}

			//JSON
			if (!File.Exists(FilePathJSON))
			{
				lock (Program.LockJson)
				{
					File.WriteAllText(FilePathJSON, "{}");
				}
			}

			Console.WriteLine("Selenium Scrapper Initialized...");

			// REMOVER COMENTARIO PARA DESATIVAR A ABERTURA DO NAVEGADOR DO NAVEGADOR
			//ChromeOptions.AddArgument("--headless");
			var driver = new ChromeDriver(ChromeOptions);

			string url = $"https://proxyservers.pro/proxy/list/order/updated/order_dir/desc/page/{startPage}";
			try
			{
				driver.Navigate().GoToUrl(url);
				while (NextPageExists)
				{
					SaveHtml(driver);
					ScrapeData(driver);
					SaveJson();
					CrawlerPagination(driver);
				}

				Program.EndCrawling = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
				string formattedDateTime = Program.EndCrawling.ToString("dd-MM-yyyy HH:mm:ss", new System.Globalization.CultureInfo("pt-BR"));

				_mediator.Publish(new CrawlerSaveEvent(ActualPage, formattedDateTime, FilePathJSON));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return;
			}
			driver.Quit();

		}


		private void SaveHtml(ChromeDriver driver)
		{
			Console.WriteLine($"html da {Thread.CurrentThread.Name} página {ActualPage}");
			string fileName = $"page_{ActualPage}.html";
			string filePath = Path.Combine(HtmlFolderName, fileName);

			if (!File.Exists(filePath))
			{
				string htmlContent = driver.PageSource;
				File.WriteAllText(filePath, htmlContent);
				Console.WriteLine($"Page {ActualPage} saved in {HtmlFolderName} folder.");
				Console.WriteLine(Thread.CurrentThread.Name);
			}
			else
			{
				Console.WriteLine($"Page {ActualPage} has already been downloaded, moving to the next page.");
				Console.WriteLine(Thread.CurrentThread.Name);
			}
		}

		private void SaveJson()
		{
			_mediator.Publish(new CrawlerJsonUpdateEvent(FilePathJSON, PageData, ActualPage, ProxyList));
		}

		private void ScrapeData(ChromeDriver driver)
		{
			var proxyTable = driver.FindElement(By.ClassName("table-hover"));
			var tableRows = proxyTable.FindElements(By.TagName("tr"));

			if (tableRows.Count > 1)
			{
				foreach (var row in tableRows.Skip(1))
				{
					var columns = row.FindElements(By.TagName("td")).ToList();

					string ipAdress = columns[1].Text;
					int port = int.Parse(columns[2].Text);
					string country = columns[3].Text;
					string protocol = columns[6].Text;

					ProxyConnection proxyConnection = new(ipAdress, port, country, protocol);
					ProxyList.Add(proxyConnection);

					Program.LinesCrawled++;
				}
				PageData[$"Page {ActualPage}"] = ProxyList;
			}
			else
			{
				Console.WriteLine("Empty Table, Crawler Stoped.");
				NextPageExists = false;
				return;
			}
		}

		private void CrawlerPagination(ChromeDriver driver)
		{
			try
			{
				var nextPageNumber = driver.FindElements(By.XPath($"//a[number(text()) >= {ActualPage + 1}]"));

				var pageNumberArray = nextPageNumber
				.Select(element => int.TryParse(element.Text, out int pageNumber) ? pageNumber : throw new PaginationException("Internal Error. Sorry for the inconvenience"))
				.Where(pageNumber => pageNumber != -1)
				.ToArray();

				if(pageNumberArray.Length > 0)
				{
					driver.Navigate().GoToUrl($"https://proxyservers.pro/proxy/list/order/updated/order_dir/desc/page/{ActualPage += 2}");
					Program.PagesCrawled++;
				}
				else
				{
					throw new PaginationException("Finished.");
				}
			}
			catch (PaginationException ex)
			{
				Console.WriteLine(ex.Message);

				NextPageExists = false;
			}
		}
	}
}
