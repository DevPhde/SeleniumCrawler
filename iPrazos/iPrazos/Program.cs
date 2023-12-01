using iPrazos.Selenium;
using IPrazos.Entity;

namespace iPrazos
{
	public class Program
	{

		public static string StartCrawling;
		public static DateTime EndCrawling;
		public static object LockObject = new();
		public static object LockJson = new();
		public static int LinesCrawled = 0;
		public static int PagesCrawled = 1;
		public static int EventCounter = 0;
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddMediatR(cfg =>
				cfg.RegisterServicesFromAssembly(typeof(SeleniumCrawler).Assembly));

			builder.Services.AddTransient<SeleniumCrawler>();
			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			var crawlerInstance1 = app.Services.GetRequiredService<SeleniumCrawler>();
			var crawlerInstance2 = app.Services.GetRequiredService<SeleniumCrawler>();

			Thread crawler1 = new(() =>
			{
				DateTime time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
				string startCrawling = time.ToString("dd-MM-yyyy HH:mm:ss", new System.Globalization.CultureInfo("pt-BR"));
				Thread.CurrentThread.Name = "Thread1";
				crawlerInstance1.Init(1);
			});
			crawler1.Start();

			Thread crawler2 = new(() =>
			{
				DateTime time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
				string startCrawling = time.ToString("dd-MM-yyyy HH:mm:ss", new System.Globalization.CultureInfo("pt-BR"));
				Thread.CurrentThread.Name = "Thread2";
				crawlerInstance2.Init(2);
			});
			crawler2.Start();


			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}