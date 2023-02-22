using Hangfire;
using Hangfire.PostgreSql;
using invest.Jobs;
using invest.Model;
using invest.Steam;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<DatabaseContext>(conf => conf.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgresConnection"), "logs", schemaName: "serilog")
    .CreateLogger();

builder.Services.AddHangfire(conf => conf
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(builder.Configuration.GetConnectionString("PostgresConnection"), new PostgreSqlStorageOptions()
    {
        InvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.FromMilliseconds(200),
        DistributedLockTimeout = TimeSpan.FromMinutes(1),
        PrepareSchemaIfNecessary = true
    })
);

builder.Services.AddScoped<SteamAuthService>();
builder.Services.AddHostedService<HangfireJobs>();

builder.Services.AddHangfireServer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
} else {
    app.UseHangfireDashboard();
    app.MapHangfireDashboard();
    app.UseSwaggerUI();
    app.MapSwagger();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();