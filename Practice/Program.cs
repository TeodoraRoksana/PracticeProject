using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Practice.Middleware;
using Practice.Models;
using Practice.Services;
using Practice.Services.DBService;
using Practice.Services.EmailService;
using Practice.Services.HashService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PracticeContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MainDB")));

// Add services to the container.
builder.Services.AddJWTAuthentication();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IDBService, DBService>();
builder.Services.AddScoped<IHashService, HashService>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);//We set Time here 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
}); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseMiddleware<MyAuthorizationMiddleware>();

app.UseAuthentication();
app.UseAuthorization();
//app.UseMiddleware<MyAuthorizationMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
