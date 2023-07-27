using ElectronNET.API;
using Raspberry.App.Database;
using Raspberry.App.Database.Model;
using Raspberry.App.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseElectron(args);

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddTransient<IDatabaseService<Item>>();
builder.Services.AddTransient<IDatabaseService<Price>>();
builder.Services.AddTransient<IDatabaseService<Order>>();
builder.Services.AddTransient<IQueryByItem<Price>>();
builder.Services.AddTransient<IQueryByItem<Order>>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

await app.StartAsync();

await Electron.WindowManager.CreateWindowAsync();

app.WaitForShutdown();
