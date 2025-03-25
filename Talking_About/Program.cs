using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Reflection;
using Talking_About.Api.Extensions;
using Talking_About.Application.UseCases.ReportUseCase.CreateUseCase;
using Talking_About.Components;
using Talking_About.Components.Account;
using Talking_About.Data;
using Talking_About.Domain.Repositories;
using Talking_About.Endpoints;
using Talking_About.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();
builder.Services.AddFluentUIComponents();
builder.Services.AddHttpContextAccessor();


builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddSignInManager()
    .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddMediatR(cfg =>
           cfg.RegisterServicesFromAssembly(typeof(CreateReportCommand).Assembly));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, EmailSenderService>();
builder.Services.AddTransient<IReportRepository, ReportRepository>();
builder.Services.AddTransient<IImageRepository, ImageRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<ILikeRepository, LikeRepository>();
builder.Services.AddTransient<IInstitutionRepository, InstitutionRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Talking_About.Client._Imports).Assembly);

//app.MapAdditionalIdentityEndpoints();
app.MapIdentityApi<ApplicationUser>();
app.UseAntiforgery();
app.MapEndpoints();

app.Run();
