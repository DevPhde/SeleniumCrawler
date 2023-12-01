using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IPrazos.Entity
{
	public class ProxyData
	{
		public ProxyData( string startCrawling, string endCrawling, List<Dictionary<string, object>> jsonData, int linesCrawled, int pagesCrawled)
		{
			StartCrawling = startCrawling;
			EndCrawling = endCrawling;
			JsonData = jsonData;
			LinesCrawled = linesCrawled;
			PagesCrawled = pagesCrawled;
		}

		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		public string StartCrawling { get; set; }
		public string EndCrawling { get; set; }
		public int LinesCrawled { get; set; }
		public int PagesCrawled { get; set; }
		public List<Dictionary<string, object>> JsonData { get; set; }
		
	}
}
