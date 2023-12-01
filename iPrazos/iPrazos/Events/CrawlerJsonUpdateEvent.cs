using IPrazos.Entity;
using MediatR;

namespace iPrazos.Events
{
	public class CrawlerJsonUpdateEvent : INotification
	{
		public string JsonPath;
		public Dictionary<string, List<ProxyConnection>> PageData = new Dictionary<string, List<ProxyConnection>>();
		public int Page;
		public List<ProxyConnection> ProxyList;

		public CrawlerJsonUpdateEvent(string jsonPath, Dictionary<string, List<ProxyConnection>> pageData, int page, List<ProxyConnection> proxyList)
		{
			JsonPath = jsonPath;
			PageData = pageData;
			Page = page;
			ProxyList = proxyList;
		}
	}
}
