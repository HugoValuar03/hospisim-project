using Hospisim.Data;
using Hospisim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hospisim.Controllers
{
    public class HomeController : Controller
    {
        // Injetamos o DbContext para acessar o banco de dados
        private readonly ApplicationDbContext _context;

        // O ILogger pode ser mantido ou removido se não for usado.
        // O ApplicationDbContext é o mais importante aqui.
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new DashboardViewModel
            {
                PacientesInternados = await _context.Internacoes.CountAsync(i => i.StatusInternacao == StatusInternacao.ATIVA),
                ProfissionaisAtivos = await _context.ProfissionaisSaude.CountAsync(p => p.Ativo),
                AtendimentosHoje = await _context.Atendimentos.CountAsync(a => a.DataEHora.Date == DateTime.Today),

                UltimasInternacoes = await _context.Internacoes
                                            .Include(i => i.Paciente)
                                            .OrderByDescending(i => i.DataEntrada)
                                            .Take(5)
                                            .ToListAsync(),

                UltimasAltas = await _context.AltasHospitalares
                                        .Include(a => a.Internacao.Paciente)
                                        .OrderByDescending(a => a.Data)
                                        .Take(5)
                                        .ToListAsync()
            };

            viewModel.LeitosOcupados = viewModel.PacientesInternados;

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}