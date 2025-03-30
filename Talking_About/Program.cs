using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using Talking_About.Components;
using Talking_About.Components.Account;
using Talking_About.Data; // Referência para ApplicationDbContext

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados e Identity para Blazor
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  // Verifique se a string de conexão está correta

// Configuração do Identity no Blazor (Descomente e ajuste conforme necessário)
builder.Services.AddIdentityCore<ApplicationUser>(options =>
    options.SignIn.RequireConfirmedAccount = true) // Você pode ajustar a configuração conforme necessário
    .AddSignInManager()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddAntiforgery();
builder.Services.AddAuthorization();

// Adicionando componentes interativos do Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();
builder.Services.AddFluentUIComponents();

// Configuração de autenticação e Identity
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
//builder.Logging.AddConsole();
builder.Services.AddAntiforgery();


builder.Services.AddSingleton<IEmailSender<ApplicationUser>, EmailSenderService>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddIdentityCookies();

// Configuração do HttpClient para se comunicar com o back-end
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7249"); // URL do seu back-end API
});

// Configuração do Blazor
var app = builder.Build();

// Aplicando autenticação e autorização no Blazor
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Configuração do pipeline de desenvolvimento (Swagger)
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// Mapeando recursos estáticos e Razor Components
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Talking_About.Client._Imports).Assembly);

app.UseAntiforgery();

await app.RunAsync();
