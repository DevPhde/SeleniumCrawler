using IPrazos.Entity;
using MediatR;

namespace Crawler.Events
{
    public class CrawlerSaveEvent : INotification
    {
        public string EndCrawlingFormattedDate;
        public string FilePathJson;

		public CrawlerSaveEvent(int pageCount, string endCrawlingFormattedDate, string filePathJson)
		{
			EndCrawlingFormattedDate = endCrawlingFormattedDate;
			FilePathJson = filePathJson;
		}
	}
}
