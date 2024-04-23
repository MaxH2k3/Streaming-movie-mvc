using SMovie.Dashboard.Hub;
using SMovie.Dashboard.Middleware;
using SMovie.Domain.Constants;
using SMovie.Infrastructure.Configuration;
using WatchDog;
using WatchDog.src.Enums;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(x =>
{
    x.Filters.Add<FilterLogs>();
});

builder.AddInfrastructure();
builder.Services.AddSignalR();
builder.Services.AddQuartzConfig(builder.Configuration);
builder.Services.AddWatchDogServices(opt =>
{
    opt.IsAutoClear = true;
    opt.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Hourly;
    opt.SetExternalDbConnString = builder.Configuration.GetConnectionString("Hangfire");
    opt.DbDriverOption = WatchDogDbDriverEnum.Mongo;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(SystemConstant.ErrorHandler);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//inject middleware watch dog logs
app.UseWatchDogExceptionLogger();
app.UseWatchDog(opt =>
{
    opt.WatchPageUsername = "admin";
    opt.WatchPagePassword = "123";
    opt.Blacklist = "Login/";
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.MapHub<NotificationHub>(SystemConstant.HubConnection);

app.Run();
