using CmsModels;
using DbContexts;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Service;
using CmsMvc.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<ISyncService<Post>, SyncService<Post>>();
builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("CmsDatabase")
        ?? "Data Source=localcms.db"));

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LocalDbContext>();
    await CmsSeed.InitializeAsync(db);
}
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LocalDbContext>();
    await CmsSeed.InitializeAsync(db);
}

await app.RunAsync();
