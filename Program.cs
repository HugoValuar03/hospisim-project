using Microsoft.EntityFrameworkCore;
using Hospisim.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Adiciona os servi�os para Controllers e Views
builder.Services.AddControllersWithViews();

// Adiciona o servi�o do seu banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configura o pipeline de requisi��es HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // O valor padr�o de HSTS � 30 dias.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Permite o uso de arquivos est�ticos como CSS e JavaScript

// 2. Adiciona o middleware de roteamento
app.UseRouting();

app.UseAuthorization();

// 3. Mapeia a rota padr�o para os controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();