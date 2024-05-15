using Mamba.Bussiness.Services.Abstratcs;
using Mamba.Bussiness.Services.Concretes;
using Mamba.Core.RepositoryAbstracts;
using Mamba.Data.DAL;
using Mamba.Data.RepositoryConcretes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer("Server=WIN-0F0TGHD6FSA\\SQLEXPRESS;Database=MambaNlayer;Trusted_Connection=true;Integrated Security=true;TrustServerCertificate=true;Encrypt=false");
});
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
