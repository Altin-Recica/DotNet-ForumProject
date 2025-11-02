using Forum.BL;
using Forum.DAL;
using Forum.DAL.EF;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<UserDbContext>(optionsBuilder => 
    optionsBuilder.UseSqlite("Data Source=User.db"));
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IManager, Manager>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

 using (var scope = app.Services.CreateScope())
 {
     UserDbContext ctx = scope.ServiceProvider.GetRequiredService<UserDbContext>();
     ctx.Database.EnsureDeleted();
     if (ctx.CreateDatabase(true))
        DataSeeder.Seed(ctx);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();