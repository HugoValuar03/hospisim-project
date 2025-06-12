using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospisim.Data;
using Hospisim.Models;

namespace Hospisim.Controllers
{
    public class InternacaosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InternacaosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Internacaos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Internacoes.Include(i => i.Atendimento).Include(i => i.Paciente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Internacaos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var internacao = await _context.Internacoes
                .Include(i => i.Atendimento)
                .Include(i => i.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (internacao == null) return NotFound();
            return View(internacao);
        }

        // GET: Internacaos/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST: Internacaos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PacienteId,AtendimentoId,DataEntrada,PrevisaoAlta,MotivoInternacao,Leito,Quarto,Setor,PlanoSaudeUtilizado,ObservacoesClinicas,StatusInternacao")] Internacao internacao)
        {
            if (ModelState.IsValid)
            {
                internacao.Id = Guid.NewGuid();
                _context.Add(internacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdowns(internacao.PacienteId, internacao.AtendimentoId);
            return View(internacao);
        }

        // GET: Internacaos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var internacao = await _context.Internacoes.FindAsync(id);
            if (internacao == null) return NotFound();
            PopulateDropdowns(internacao.PacienteId, internacao.AtendimentoId);
            return View(internacao);
        }

        // POST: Internacaos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PacienteId,AtendimentoId,DataEntrada,PrevisaoAlta,MotivoInternacao,Leito,Quarto,Setor,PlanoSaudeUtilizado,ObservacoesClinicas,StatusInternacao")] Internacao internacao)
        {
            if (id != internacao.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(internacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InternacaoExists(internacao.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdowns(internacao.PacienteId, internacao.AtendimentoId);
            return View(internacao);
        }

        // GET: Internacaos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var internacao = await _context.Internacoes
                .Include(i => i.Atendimento)
                .Include(i => i.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (internacao == null) return NotFound();
            return View(internacao);
        }

        // POST: Internacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var internacao = await _context.Internacoes.FindAsync(id);
            if (internacao != null)
            {
                _context.Internacoes.Remove(internacao);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Método privado para popular os dropdowns, evitando repetição de código
        private void PopulateDropdowns(object selectedPaciente = null, object selectedAtendimento = null)
        {
            var pacientes = _context.Pacientes.OrderBy(p => p.NomeCompleto).ToList();
            ViewData["PacienteId"] = new SelectList(pacientes, "Id", "NomeCompleto", selectedPaciente);

            var atendimentos = _context.Atendimentos.Include(a => a.Paciente).ToList();
            var listaAtendimentos = atendimentos.Select(a => new {
                Value = a.Id,
                Text = $"ID: {a.Id} (Paciente: {a.Paciente?.NomeCompleto ?? "N/A"})"
            });
            ViewData["AtendimentoId"] = new SelectList(listaAtendimentos, "Value", "Text", selectedAtendimento);
        }

        private bool InternacaoExists(Guid id)
        {
            return _context.Internacoes.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetPacienteDetailsJson(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null) return NotFound();
            return Json(paciente);
        }
    }
}