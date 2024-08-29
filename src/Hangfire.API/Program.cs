using Hangfire;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var hangFireConnectionString = builder.Configuration.GetConnectionString("HangfireConnection");
builder.Services.AddHangfire(configuration => configuration
	.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
	.UseSimpleAssemblyNameTypeSerializer()
	.UseRecommendedSerializerSettings()
	.UseSqlServerStorage(hangFireConnectionString)
);
builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseHangfireDashboard("/hangfire", new DashboardOptions 
	{
		Authorization = new [] { new MyAuthorizationFilter()}
	});
	app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapControllers();
app.MapHangfireDashboard();

RecurringJob.AddOrUpdate(
	"minha-tarefa-recorrente",
	() => Console.WriteLine("Hello world from Hangfire Recurring!"),
Cron.Minutely
);

BackgroundJob.Enqueue(() => new Testinho().JobDoido());
BackgroundJob.Schedule(
	() => Console.WriteLine("Hello world from Hangfire Scheduled!"),
	TimeSpan.FromMinutes(2)
);

app.Run();

record Testinho()
{
	public async Task JobDoido()
	{
		await Task.Delay(TimeSpan.FromSeconds(10));
		Console.WriteLine("Hello world from Hangfire Enqueue!");
	}
}
