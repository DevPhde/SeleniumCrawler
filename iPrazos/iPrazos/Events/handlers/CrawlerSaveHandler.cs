using Crawler.Events;
using Crawler.Repository;
using IPrazos.Entity;
using MediatR;
using Newtonsoft.Json.Linq;

namespace iPrazos.Events.handlers
{
	public class CrawlerSaveHandler : INotificationHandler<CrawlerSaveEvent>
	{
		public Task Handle(CrawlerSaveEvent notification, CancellationToken cancellationToken)
		{
			Program.EventCounter++;
			if(Program.EventCounter == 2)
			{
				Program.EndCrawling = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
				string formattedDateTime = Program.EndCrawling.ToString("dd-MM-yyyy HH:mm:ss", new System.Globalization.CultureInfo("pt-BR"));

				string jsonPath = "proxyList.json";
				string jsonContent = File.ReadAllText(jsonPath);
				var json = JObject.Parse(jsonContent);

				List<Dictionary<string, object>> proxyDataList = new List<Dictionary<string, object>>();
				foreach (var page in json)
				{
					var proxies = page.Value as JArray;
					if (proxies != null)
					{
						foreach (var proxy in proxies)
						{
							Dictionary<string, object> proxyDict = new Dictionary<string, object>
							{
								{ "IpAdress", proxy["IpAdress"]?.ToString() },
								{ "Port", proxy["Port"]?.ToObject<int>() ?? 0 },
								{ "Country", proxy["Country"]?.ToString() },
								{ "Protocol", proxy["Protocol"]?.ToString() }
							};

							proxyDataList.Add(proxyDict);
						}
					}
				}
			
			Console.WriteLine(Program.StartCrawling);
            ProxyData proxyData = new(Program.StartCrawling, formattedDateTime, proxyDataList, Program.LinesCrawled, Program.PagesCrawled);
			_ = ProxyDataRepository.SaveProxyData(proxyData);
			}
			return Task.CompletedTask;
		}

	}
}
