using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ppedv.PizzaPizzaPizza.Model.Contracts;
using ppedv.PizzaPizzaPizza.UI.BlazorWeb.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

string conString = "Server=(localdb)\\mssqllocaldb;Database=PizzaPizzaPizza_dev;Trusted_Connection=true;";
builder.Services.AddScoped<IRepository>(x => new ppedv.PizzaPizzaPizza.Data.EfCore.EfRepository(conString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
