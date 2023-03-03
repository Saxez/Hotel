
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project1.Models;
using System.Security.Claims;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Authorization;
using System.Data;


var builder = WebApplication.CreateBuilder();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/accessdenied";
    });

string connectionS = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionS));


builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();



var app = builder.Build();


using (AppDbContext db = new AppDbContext())
{
    User user1 = new User { First_name = "Tom", Email = "Tom@gmail.com", Last_name = "Tomasson", Password = "1234", Role = "admin" };
    User user2 = new User { First_name = "John", Email = "John@gmail.com", Last_name = "Johnson", Password = "4321", Role = "manager" };
    User user3 = new User { First_name = "Smith", Email = "Smith@gmail.com", Last_name = "Smithson", Password = "1111", Role = "manager" };
    User user4 = new User { First_name = "Main", Email = "Main@gmail.com", Last_name = "Mainson", Password = "1111", Role = "main manager" };

    MassEvent massEvent = new MassEvent { DateOfEnd = DateTime.UtcNow, DateOfStart = DateTime.UtcNow, Name = "Event 1", Description = "test desc" };

    MassEvent massEvent2 = new MassEvent { DateOfEnd = DateTime.UtcNow, DateOfStart = DateTime.UtcNow, Name = "Event 2", Description = "test desc2" };

    Groups group1 = new Groups { Count = 5, MassEvent = massEvent2, User = user2};
    Groups group2 = new Groups { Count = 1, MassEvent = massEvent2, User = user3};


    Hotel hotel1 = new Hotel { Name = "Hotel 1", MassEvent = massEvent };
    Hotel hotel2 = new Hotel { Name = "Hotel 2", MassEvent = massEvent };
    Hotel hotel3 = new Hotel { Name = "Hotel 3", MassEvent = massEvent };
    Hotel hotel4 = new Hotel { Name = "Hotel 4", MassEvent = massEvent2 };
    db.Users.AddRange(user1, user2, user3);
    db.MassEvents.AddRange(massEvent, massEvent2);
    db.Hotels.AddRange(hotel1, hotel2, hotel3, hotel4);
    db.Groups.AddRange(group1, group2);
    db.SaveChanges();
}
app.UseAuthentication();
app.UseAuthorization();   // добавление middleware авторизации 

app.MapGet("/accessdenied", async (HttpContext context) =>
{
    context.Response.StatusCode = 403;
    await context.Response.WriteAsync("Access Denied");
});
app.MapGet("/login", async (HttpContext context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    // html-форма для ввода логина/пароля
    string loginForm = @"<!DOCTYPE html>
    <html>
    <head>
        <meta charset='utf-8' />
        <title>Login form</title>
    </head>
    <body>
        <h2>Login Form</h2>
        <form method='post'>
            <p>
                <label>Email</label><br />
                <input name='email' />
            </p>
            <p>
                <label>Password</label><br />
                <input type='password' name='password' />
            </p>
            <input type='submit' value='Login' />
        </form>
    </body>
    </html>";
    await context.Response.WriteAsync(loginForm);
});

app.MapPost("/login", async (string? returnUrl, HttpContext context, AppDbContext db) =>
{
    // получаем из формы email и пароль
    var form = context.Request.Form;
    // если email и/или пароль не установлены, посылаем статусный код ошибки 400
    if (!form.ContainsKey("email") || !form.ContainsKey("password"))
        return Results.BadRequest("Email и/или пароль не установлены");
    string email = form["email"];
    string password = form["password"];

    // находим пользователя 
    User? User = db.Users.ToList().FirstOrDefault(p => p.Email == email && p.Password == password);
    // если пользователь не найден, отправляем статусный код 401
    if (User is null) return Results.Unauthorized();
    var claims = new List<Claim>
    {
        new Claim(ClaimsIdentity.DefaultNameClaimType, User.Email),
        new Claim(ClaimsIdentity.DefaultRoleClaimType, User.Role),
        new Claim("Id", User.Id.ToString()),
    };
    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
    await context.SignInAsync(claimsPrincipal);
    return Results.Redirect(returnUrl ?? "/");
});
// доступ только для роли admin
app.Map("/admin", [Authorize(Roles = "admin")] () => "Admin Panel");


app.Map("/hotels", [Authorize] (AppDbContext db) =>
{
    string stringOut = "";
    var hotels = db.Hotels.Include(u => u.MassEvent).ToList();
    foreach (Hotel hotel in hotels)
        stringOut = stringOut + ($"{hotel.Name} - {hotel.MassEvent?.Name}");
    return stringOut;
});

app.Map("/events", (AppDbContext db) =>
{
    string stringOut = "";
    var MassEvents = db.MassEvents.Include(c => c.Hotels).ToList();
    foreach (MassEvent Event in MassEvents)
    {
        stringOut = stringOut + ($"\n Event : {Event.Name}");
        foreach (Hotel hotel in Event.Hotels)
        {
            stringOut = stringOut + ($" hotel: {hotel.Name}");
        }
    }
    return stringOut;
});

app.Map("/my_groups", [Authorize] (AppDbContext db, HttpContext con) =>
{
    var id = con.User.FindFirstValue("Id");
    var user = (from users in db.Users where users.Id.ToString() == id select users);
    string stringOut = "";
    var my_groups = (from groups in db.Groups
                     where groups.User == user
                  select groups).ToList();
    foreach (var group in my_groups)
    {
        stringOut = stringOut + ($"; {group.Count.ToString()}");
    }

    return stringOut;
});

app.Map("/", [Authorize] () => $"Hello World!");

app.MapGet("/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return "Данные удалены";
});

app.Run();

