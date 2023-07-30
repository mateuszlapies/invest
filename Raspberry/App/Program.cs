using ElectronNET.API;
using ElectronNET.API.Entities;
using Raspberry.App.Database;
using Raspberry.App.Services.Interfaces;
using Raspberry.App.Services.Database;
using Raspberry.App.Model.Database;
using Raspberry.App.Model.Services.Stats;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseElectron(args);

builder.Services.AddControllersWithViews();

builder.Services.AddElectron();

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddTransient<IDatabaseService<Item>, ItemService>();
builder.Services.AddTransient<IDatabaseService<Price>, PriceService>();
builder.Services.AddTransient<IDatabaseService<Order>, OrderService>();

builder.Services.AddTransient<IQueryAllByItem<Price>, PriceService>();
builder.Services.AddTransient<IQueryAllByItem<Order>, OrderService>();

builder.Services.AddTransient<IQueryByItem<Stats>, StatsService>();

builder.Services.AddTransient<IChartService, ChartService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapFallbackToFile("index.html"); ;

await app.StartAsync();

if (!app.Environment.IsDevelopment())
{
    var config = app.Configuration.GetSection("BrowserWindowOptions").Get<BrowserWindowOptions>();
    await Electron.WindowManager.CreateWindowAsync(config);
}

app.WaitForShutdown();
