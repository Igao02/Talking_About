using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talking_About.Api.Extensions;
using Talking_About.Application.UseCases.ReportUseCase.CreateUseCase;
using Talking_About.Data;
using Talking_About.Domain.Repositories;
using Talking_About.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do banco de dados e da autentica��o
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//builder.Services.AddAntiforgery();


builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateReportCommand).Assembly));

// Configura��o da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro dos servi�os de Identity
builder.Services.AddIdentityCore<ApplicationUser>(options =>
    options.SignIn.RequireConfirmedAccount = true) // Voc� pode ajustar a configura��o conforme necess�rio
    .AddSignInManager()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// Configura��o de Autentica��o e Autoriza��o
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddIdentityCookies();
builder.Services.AddAuthorization();


// Reposit�rios
builder.Services.AddTransient<IReportRepository, ReportRepository>();
builder.Services.AddTransient<IImageRepository, ImageRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<ILikeRepository, LikeRepository>();
builder.Services.AddTransient<IInstitutionRepository, InstitutionRepository>();

// Registro do servi�o de envio de e-mail
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, EmailSenderService>();

var app = builder.Build();

// Configura��o do pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
//app.UseAntiforgery();
app.UseHttpsRedirection();

// Configurar os endpoints da API
app.MapIdentityApi<ApplicationUser>();
app.MapEndpoints();

await app.RunAsync();
