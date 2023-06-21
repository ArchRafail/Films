using Films.Interfaces;
using Films.Repositories;
using Films.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IFilmsService, FilmsServices>();
builder.Services.AddScoped<IFilmsRepository, FilmsRepositories>();

// Add services to the container.
builder.Services.AddRazorPages();

var databaseConnectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<FilmsDbContext>(opts =>
opts.UseSqlServer(databaseConnectionString));

var app = builder.Build();

await DataBaseInitializer.InitializeDatabaseAsync(app);

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
