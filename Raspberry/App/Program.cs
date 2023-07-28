using ElectronNET.API;
using Raspberry.App.Database.Model;
using Raspberry.App.Database;
using Raspberry.App.Services.Interfaces;
using Raspberry.App.Services.Database;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseElectron(args);

builder.Services.AddControllersWithViews();

builder.Services.AddElectron();

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddTransient<IDatabaseService<Item>, ItemService>();
builder.Services.AddTransient<IDatabaseService<Price>, PriceService>();
builder.Services.AddTransient<IDatabaseService<Order>, OrderService>();

builder.Services.AddTransient<IQueryByItem<Price>, PriceService>();
builder.Services.AddTransient<IQueryByItem<Order>, OrderService>();

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

await Electron.WindowManager.CreateWindowAsync();

app.WaitForShutdown();
