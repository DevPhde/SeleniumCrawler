using IPrazos.Entity;
using MediatR;

namespace Crawler.Events
{
    public class CrawlerSaveEvent : INotification
    {
        public string FilePathJson;

		public CrawlerSaveEvent(string filePathJson)
		{
			FilePathJson = filePathJson;
		}
	}
}
