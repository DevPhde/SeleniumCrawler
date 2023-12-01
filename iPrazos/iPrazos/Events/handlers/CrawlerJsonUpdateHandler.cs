using IPrazos.Entity;
using MediatR;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace iPrazos.Events.handlers
{
	public class CrawlerJsonUpdateHandler : INotificationHandler<CrawlerJsonUpdateEvent>
	{
		private static Semaphore Semaphore = new Semaphore(1, 1);
		public Task Handle(CrawlerJsonUpdateEvent notification, CancellationToken cancellationToken)
		{
			Semaphore.WaitOne();
			try { 
				string currentJson = File.ReadAllText(notification.JsonPath);
				var existingData = JsonConvert.DeserializeObject<ConcurrentDictionary<string, List<ProxyConnection>>>(currentJson);
				foreach (var kvp in notification.PageData)
				{
					existingData[kvp.Key] = kvp.Value;
				}
				string updatedJson = JsonConvert.SerializeObject(existingData, Formatting.Indented);
				File.WriteAllText(notification.JsonPath, updatedJson);
				
			}
			finally
			{
				Semaphore.Release();

			}
			return Task.CompletedTask;
		}
	}
}
