using Microsoft.AspNetCore.Authentication.Cookies;
using MVCAuth.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option=>
    {
        option.LoginPath = "/Login/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        option.LogoutPath = "/Home/Logout";
        option.AccessDeniedPath= "/Home/AccessDenied";
    });
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1800);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddSingleton<AccountService>();
builder.Services.AddSingleton<MSASignInManager>();
builder.Services.AddSingleton<UserManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

//app.UseAuthentication();    
//chuyen qua su dung Middleware tu viet
app.UseMiddleware<MSAAuthenticationMW>();

//app.UseAuthorization();
app.UseMiddleware<MSAAuthorizationMW>();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
