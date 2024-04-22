using SMovie.Dashboard.Hub;
using SMovie.Dashboard.Middleware;
using SMovie.Domain.Constants;
using SMovie.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(x =>
{
    x.Filters.Add(new FilterLogs());
});

builder.AddInfrastructure();
builder.Services.AddSignalR();
builder.Services.AddQuartzConfig(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(SystemConstant.ErrorHandler);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

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
