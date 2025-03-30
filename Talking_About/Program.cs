using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using Talking_About.Components;
using Talking_About.Components.Account;
using Talking_About.Data; // Refer�ncia para ApplicationDbContext

var builder = WebApplication.CreateBuilder(args);

// Configura��o do banco de dados e Identity para Blazor
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  // Verifique se a string de conex�o est� correta

// Configura��o do Identity no Blazor (Descomente e ajuste conforme necess�rio)
builder.Services.AddIdentityCore<ApplicationUser>(options =>
    options.SignIn.RequireConfirmedAccount = true) // Voc� pode ajustar a configura��o conforme necess�rio
    .AddSignInManager()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//builder.Services.AddSwaggerGen();
builder.Services.AddAntiforgery();



// Adicionando componentes interativos do Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();
builder.Services.AddFluentUIComponents();

// Configura��o de autentica��o e Identity
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

// Configura��o do HttpClient para se comunicar com o back-end
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7249"); // URL do seu back-end API
});

// Configura��o do Blazor
var app = builder.Build();

// Aplicando autentica��o e autoriza��o no Blazor
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

// Configura��o do pipeline de desenvolvimento (Swagger)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Mapeando recursos est�ticos e Razor Components
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Talking_About.Client._Imports).Assembly);

await app.RunAsync();
