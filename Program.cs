using Microsoft.EntityFrameworkCore;
using Hospisim.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Adiciona os serviços para Controllers e Views
builder.Services.AddControllersWithViews();

// Adiciona o serviço do seu banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configura o pipeline de requisições HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // O valor padrão de HSTS é 30 dias.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Permite o uso de arquivos estáticos como CSS e JavaScript

// 2. Adiciona o middleware de roteamento
app.UseRouting();

app.UseAuthorization();

// 3. Mapeia a rota padrão para os controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();