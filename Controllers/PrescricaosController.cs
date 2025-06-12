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
    public class PrescricaosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrescricaosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prescricaos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Prescricoes
                .Include(p => p.Atendimento)
                    .ThenInclude(a => a.Paciente)
                .Include(p => p.ProfissionalSaude);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Prescricaos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescricao = await _context.Prescricoes
                .Include(p => p.Atendimento)
                .Include(p => p.ProfissionalSaude)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prescricao == null)
            {
                return NotFound();
            }

            return View(prescricao);
        }

        // GET: Prescricaos/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST: Prescricaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AtendimentoId,ProfissionalSaudeId,Medicamento,Dosagem,Frequencia,ViaAdministracao,DataInicio,DataFim,Observacoes,StatusPrescricao,ReacoesAdversas")] Prescricao prescricao)
        {
            if (ModelState.IsValid)
            {
                prescricao.Id = Guid.NewGuid();
                _context.Add(prescricao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdowns(prescricao.AtendimentoId, prescricao.ProfissionalSaudeId);
            return View(prescricao);
        }

        // GET: Prescricaos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescricao = await _context.Prescricoes.FindAsync(id);
            if (prescricao == null)
            {
                return NotFound();
            }
            ViewData["AtendimentoId"] = new SelectList(_context.Atendimentos, "Id", "Id", prescricao.AtendimentoId);
            ViewData["ProfissionalSaudeId"] = new SelectList(_context.ProfissionaisSaude, "Id", "Cpf", prescricao.ProfissionalSaudeId);
            return View(prescricao);
        }

        // POST: Prescricaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,AtendimentoId,ProfissionalSaudeId,Medicamento,Dosagem,Frequencia,ViaAdministracao,DataInicio,DataFim,Observacoes,StatusPrescricao,ReacoesAdversas")] Prescricao prescricao)
        {
            if (id != prescricao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prescricao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrescricaoExists(prescricao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AtendimentoId"] = new SelectList(_context.Atendimentos, "Id", "Id", prescricao.AtendimentoId);
            ViewData["ProfissionalSaudeId"] = new SelectList(_context.ProfissionaisSaude, "Id", "Cpf", prescricao.ProfissionalSaudeId);
            return View(prescricao);
        }

        // GET: Prescricaos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescricao = await _context.Prescricoes
                .Include(p => p.Atendimento)
                .Include(p => p.ProfissionalSaude)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prescricao == null)
            {
                return NotFound();
            }

            return View(prescricao);
        }

        // POST: Prescricaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var prescricao = await _context.Prescricoes.FindAsync(id);
            if (prescricao != null)
            {
                _context.Prescricoes.Remove(prescricao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropdowns(object? selectedAtendimento = null, object? selectedProfissional = null)
        {
            var atendimentos = _context.Atendimentos.Include(a => a.Paciente).OrderByDescending(a => a.DataEHora).ToList();
            var listaAtendimentos = atendimentos.Select(a => new {
                Value = a.Id,
                Text = $"ID: {a.Id} (Paciente: {a.Paciente?.NomeCompleto ?? "N/A"})"
            });
            ViewData["AtendimentoId"] = new SelectList(listaAtendimentos, "Value", "Text", selectedAtendimento);

            var profissionais = _context.ProfissionaisSaude.OrderBy(p => p.NomeCompleto).ToList();
            ViewData["ProfissionalSaudeId"] = new SelectList(profissionais, "Id", "NomeCompleto", selectedProfissional);
        }

        private bool PrescricaoExists(Guid id)
        {
            return _context.Prescricoes.Any(e => e.Id == id);
        }
    }
}
