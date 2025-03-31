using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talking_About.Api.Endpoints.Reports;
using Talking_About.Api.Extensions;
using Talking_About.Application.UseCases.ReportUseCase.CreateUseCase;
using Talking_About.Data;
using Talking_About.Domain.Repositories;
using Talking_About.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Banco de Dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configuração de Identity
builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddSignInManager()
.AddRoles<IdentityRole>()
.AddDefaultTokenProviders()
.AddEntityFrameworkStores<ApplicationDbContext>();

// Autenticação e Autorização
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddIdentityCookies();

builder.Services.AddAuthorization();

// Serviços da Aplicação
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateReportCommand).Assembly));

builder.Services.AddTransient<IReportRepository, ReportRepository>();
builder.Services.AddTransient<IImageRepository, ImageRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<ILikeRepository, LikeRepository>();
builder.Services.AddTransient<IInstitutionRepository, InstitutionRepository>();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, EmailSenderService>();

// Configuração de Swagger e API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro de Endpoints
builder.Services.AddEndpoints(typeof(CreateReportEndpoint).Assembly);

builder.Services.AddAntiforgery();

var app = builder.Build();

// Configuração do pipeline de requisição
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

// Mapeando a API
app.MapIdentityApi<ApplicationUser>();

// Mapear os endpoints definidos na classe CreateReportEndpoint
app.MapEndpoints();

await app.RunAsync();
