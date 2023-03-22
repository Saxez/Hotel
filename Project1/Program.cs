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
using System.Linq;
using Project1.Database;
using Project1.Migrations;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using Microsoft.OpenApi.Models;
using Project1.Data;

const string ACCESS_DENIED_PATH = "/accessdenied";
const string LOGIN_MAP = "/login";
const string LOGOUT_PATH = "/logout";
const string ADMIN_MAP = "/admin";
const string HOTELS_MAP = "/hotels";
const string EVENTS_MAP = "/events";
const string MY_GROUPS_MAP = "/my_groups";
const string INFO_MAP = "/info";
const string REG_USERS = "/reg_users";
const string REG_SETTLERS = "/reg_settlers";

const string LOGOUT_SIGN = "Data deleted";
const string ACCESS_DENIED = "Access Denied";
const string BAD_REQUEST_EMAIL_OR_PASSWORD = "Email and/or password are not set";
const string EMAIL = "email";
const string PASSWORD = "password";
const string FIRST_NAME = "first_name";
const string LAST_NAME = "last_name";
const string GENDER = "gender";
const string ADDITIONAL_PEOPLE = "additional_people";
const string PREFFERED_TYPE = "preffered_type";

const string ADMIN_ROLE = "admin";
const string AMBAS_ROLE = "hotel_ambas";
const string ADMIN_AND_AMBAS_ROLES = "admin, hotel_ambas";
const string MANAGER_ROLE = "manager";
const string ADMIN_AND_MANAGER_ROLES = "admin, manager";
const string MAIN_MANAGER_ROLE = "main_manager";
const string ADMIN_AND_MAIN_MANAGER_ROLEs = "admin, main_manager";
const int ACCESS_DENIED_CODE = 403;

const string LOGIN_FORM = @"<!DOCTYPE html>
<html>
    <head>
        <meta charset='utf-8' />
        <title>Login Form</title>
    </head>
    <body>
        <h2>Login Form</h2>
        <Form method='post'>
            <p>
                <label>Email</label><br />
                <input name='email' />
            </p>
            <p>
                <label>Password</label><br />
                <input type='password' name='password' />
            </p>
            <input type='submit' value='Login' />
        </Form>
    </body>
</html>";

const string REG_SETTLERS_FORM = @"<!DOCTYPE html>
<html>
    <head>
        <meta charset='utf-8' />
        <title>Registration Form</title>
    </head>
    <body>
        <h2>Registration Form</h2>
        <Form method='post'>
            <p>
                <label>First Name</label><br />
                <input name='first_name' />
            </p>
            <p>
                <label>Last Name</label><br />
                <input name='last_name' />
            </p>
            <p>
                <label>Email</label><br />
                <input name='email' />
            </p>
            <p>
                <label>Gender</label><br />
                <input name='gender' />
            </p>
            <p>
                <label>Additional people</label><br />
                <input name='additional_people' />
            </p>
            <p>
                <label>Preffered type of room</label><br />
                <input name='preffered_type' />
            </p>
            <input type='submit' value='Accept' />
        </Form>
    </body>
</html>";

const string CONTENT_TYPE = "text/html; charset=utf-8";


var Builder = WebApplication.CreateBuilder();
Builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(Options =>
    {
        Options.LoginPath = LOGIN_MAP;
        Options.AccessDeniedPath = ACCESS_DENIED_PATH;
    });

string ConnectionS = Builder.Configuration.GetConnectionString("DefaultConnection");

Builder.Services.AddDbContext<AppDbContext>(Options =>
    Options.UseSqlServer(ConnectionS));

Builder.Services.AddAuthorization();


Builder.Services.AddDistributedMemoryCache();

Builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//InitData Init = new InitData(new AppDbContext());


Builder.Services.AddControllersWithViews();

var App = Builder.Build();

App.UseAuthentication();
App.UseAuthorization();
App.UseSession();

App.MapGet(ACCESS_DENIED_PATH, async (HttpContext Context) =>
{
    Context.Response.StatusCode = ACCESS_DENIED_CODE;
    await Context.Response.WriteAsync(ACCESS_DENIED);
});
App.MapGet(LOGIN_MAP, async (HttpContext Context) =>
{
    Context.Response.ContentType = CONTENT_TYPE;

    await Context.Response.WriteAsync(LOGIN_FORM);
});

App.MapPost(LOGIN_MAP, async (string? returnUrl, HttpContext Context) =>
{
    using (var Db = new AppDbContext())
    {
        var Form = Context.Request.Form;
        if (!Form.ContainsKey(EMAIL) || !Form.ContainsKey(PASSWORD))
            return Results.BadRequest(BAD_REQUEST_EMAIL_OR_PASSWORD);
        string email = Form[EMAIL];
        string password = Form[PASSWORD];

        User? User = Db.Users.ToList().FirstOrDefault(p => p.Email == email && p.Password == password);

        if (User is null) return Results.Unauthorized();
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, User.Email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, User.Role),
            new Claim("Id", User.Id.ToString()),
        };
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        await Context.SignInAsync(claimsPrincipal);
        return Results.Redirect(returnUrl ?? INFO_MAP);
    }
});

App.MapGet(ADMIN_MAP, [Authorize(Roles = ADMIN_ROLE)] (AppDbContext Db) =>
{
    return (Repository.GetAllUsers());
});


App.MapGet(HOTELS_MAP, [Authorize] async() =>
{
    string StringOut = "";
    List<Hotel> Hotels = Repository.GetAllHotels();
    foreach (Hotel Hotel in Hotels)
        StringOut = StringOut + ($"{Hotel.Name} - {Hotel.MassEvent?.Name}\n");
    return StringOut;

});

App.MapGet(EVENTS_MAP, (AppDbContext Db) =>
{
    string StringOut = "";
    List<MassEvent> MassEvents = Repository.GetAllEvents();
    foreach (MassEvent Event in MassEvents)
    {
        StringOut = StringOut + ($"\n Event : {Event.Name}");
    }
    return StringOut;
});

App.MapGet(MY_GROUPS_MAP, [Authorize] (AppDbContext Db, HttpContext Context) =>
{
    string StringOut = "";
    string id = Context.User.FindFirstValue("Id");
    
    List<Groups> my_groups = Db.Groups.Include(group => group.Manager).Where(group => group!.Manager.Id.ToString() == id).ToList();
    foreach (Groups group in my_groups)
    {
        StringOut = StringOut + $"{group.Count} ;";
    }
    return StringOut;
});

App.MapGet("/", [Authorize] () => {
    return Results.Redirect(INFO_MAP);
});

App.MapGet(INFO_MAP, [Authorize] () => $"Hello World!");

App.MapGet(LOGOUT_PATH, async (HttpContext Context) =>
{
    await Context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return LOGOUT_SIGN;
});

App.MapPost(REG_SETTLERS, [Authorize(Roles = (ADMIN_AND_MANAGER_ROLES))] async (string? returnUrl, HttpContext Context, AppDbContext Db) =>
{
    var Form = Context.Request.Form;
    if (!Form.ContainsKey(FIRST_NAME) || !Form.ContainsKey(LAST_NAME) || !Form.ContainsKey(EMAIL) || !Form.ContainsKey(GENDER) || !Form.ContainsKey(ADDITIONAL_PEOPLE) || !Form.ContainsKey(PREFFERED_TYPE))
        return Results.BadRequest(BAD_REQUEST_EMAIL_OR_PASSWORD);
    string Email = Form[EMAIL];
    string FirstName = Form[FIRST_NAME];
    string LastName = Form[LAST_NAME];
    string GenderSt = Form[GENDER];
    string AdditionalPeople = Form[ADDITIONAL_PEOPLE];
    string PrefferedType = Form[PREFFERED_TYPE];
    int Gender = 0;
    if (GenderSt == "male") { Gender= 1; }
    Groups Unallocated = Repository.GetUnallocatedGroup();
    Settler Settler = new Settler { FirstName = FirstName, LastName = LastName, AdditionalPeoples = Int32.Parse(AdditionalPeople), Email = Email, Gender = Gender, PreferredType = PrefferedType, Groups = Unallocated};
    Db.Settler.Add(Settler);

    Db.SaveChanges();

    return Results.Redirect(returnUrl ?? INFO_MAP);
}
);


App.MapGet(REG_SETTLERS, [Authorize(Roles = (ADMIN_AND_MANAGER_ROLES))] async(HttpContext Context) =>
{
    Context.Response.ContentType = CONTENT_TYPE;

    await Context.Response.WriteAsync(REG_SETTLERS_FORM);
}
);

App.Run();



