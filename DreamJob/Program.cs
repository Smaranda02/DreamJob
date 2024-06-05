using DreamJob.BusinessLogic.Employers;
using DreamJob.BusinessLogic.Users;
using DreamJob.BusinessLogic.Candidates;
using DreamJob.BusinessLogic.Skills;
using DreamJob.DataAccess.EntityFramework;
using DreamJob.Entities.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using DreamJob.BusinessLogic.Skills;
using DreamJob.BusinessLogic.Users.ViewModels;
using System.Security.Claims;
using DreamJob.Common.Enums;
using DreamJob.BusinessLogic.Studies;
using DreamJob.BusinessLogic.Experiences;
using DreamJob.BusinessLogic.CareerFields;
using DreamJob.BusinessLogic.JobOffers;
using DreamJob.BusinessLogic.Interactions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/User/Login";
        options.AccessDeniedPath = "/User/Unauthorized";
    });


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<DreamJobContext>();


builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<JobOfferService>();
builder.Services.AddScoped<EmployerService>();
builder.Services.AddScoped<CandidateService>();
builder.Services.AddScoped<SkillsService>();
builder.Services.AddScoped<StudyService>();
builder.Services.AddScoped<ExperienceService>();

builder.Services.AddScoped<RegisterValidator>();



builder.Services.AddScoped(s =>
{
    var accessor = s.GetRequiredService<IHttpContextAccessor>();
    var httpContext = accessor.HttpContext;
    if (httpContext?.User?.Identity?.IsAuthenticated == true)
    {
        var claims = httpContext.User.Claims;
        var userId = claims.FirstOrDefault(c => c.Type == "Id")?.Value;
        var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var username = claims.FirstOrDefault(c => c.Type == "Username")?.Value;
        var role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        Enum.TryParse<Roles>(role, out var roleEnum);


        return new CurrentUserViewModel
        {
            Id = int.Parse(userId ?? "0"),
            IsAuthenticated = true,
            Email = email,
            Username = username,
            Role = (int)roleEnum,
        };
    }
    return new CurrentUserViewModel { IsAuthenticated = false };
});


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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=JobOffer}/{action=GetAllJobOffers}/{id?}");

app.Run();
