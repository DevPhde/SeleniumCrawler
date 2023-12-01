using IPrazos.Entity;
using MediatR;

namespace iPrazos.Events
{
	public class CrawlerJsonUpdateEvent : INotification
	{
		public string JsonPath;
		public Dictionary<string, List<ProxyConnection>> PageData = new Dictionary<string, List<ProxyConnection>>();
		public int Page;

		public CrawlerJsonUpdateEvent(string jsonPath, Dictionary<string, List<ProxyConnection>> pageData, int page)
		{
			JsonPath = jsonPath;
			PageData = pageData;
			Page = page;
		}
	}
}
