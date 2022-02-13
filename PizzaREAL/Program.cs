using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PizzaREAL.ModelsIdentity;
using PizzaREAL.Models;
using PizzaREAL.Services;

//Service container
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

//Hämtar connectionsträng och skapar upp en EF Service
var conn = builder.Configuration.GetConnectionString("ConString");
builder.Services.AddDbContext<PizzaDbContext>(options => options.UseSqlServer(conn));

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer
(builder.Configuration.GetConnectionString("ConString")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

builder.Services.AddTransient<IIngredientService, IngredientService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IDishService, DishService>();
builder.Services.AddTransient<IOrderService, OrderService>();

//Request pipeline (Hur en request hanteras)
var app = builder.Build();

app.UseAuthentication();

app.UseStaticFiles();
app.UseSession();


//Alternativ startroute, här kan en annan controller och action anges
app.UseMvc(routes =>
{
    routes.MapRoute(
      name: "Default",
      template: "{controller=Home}/{action=Index}"
    );
});

app.Run();
