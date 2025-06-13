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
    public class AtendimentoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AtendimentoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Atendimentoes
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var atendimentos = _context.Atendimentos
                .Include(a => a.Paciente)
                .Include(a => a.ProfissionalSaude)
                .AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                atendimentos = atendimentos.Where(a => a.Paciente.NomeCompleto.Contains(searchString)
                                                    || a.ProfissionalSaude.NomeCompleto.Contains(searchString));
            }

            return View(await atendimentos.OrderByDescending(a => a.DataEHora).ToListAsync());
        }

        // GET: Atendimentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atendimento = await _context.Atendimentos
                .Include(a => a.Paciente)
                .Include(a => a.ProfissionalSaude)
                .Include(a => a.Prontuario)
                    .ThenInclude(p => p.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (atendimento == null)
            {
                return NotFound();
            }

            return View(atendimento);
        }

        // GET: Atendimentoes/Create
        public IActionResult Create()
        {
            // O método Create agora também usa o nosso novo helper!
            PopulateDropdowns();
            return View();
        }

        // POST: Atendimentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataEHora,Tipo,Status,Local,PacienteId,ProfissionalSaudeId,ProntuarioId")] Atendimento atendimento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(atendimento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Se der erro, repopula os dropdowns da forma correta.
            PopulateDropdowns(atendimento.PacienteId, atendimento.ProfissionalSaudeId, atendimento.ProntuarioId);
            return View(atendimento);
        }

        // GET: Atendimentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atendimento = await _context.Atendimentos.FindAsync(id);
            if (atendimento == null)
            {
                return NotFound();
            }
            // Chama o helper para popular os dropdowns, já selecionando os valores atuais.
            PopulateDropdowns(atendimento.PacienteId, atendimento.ProfissionalSaudeId, atendimento.ProntuarioId);
            return View(atendimento);
        }

        // POST: Atendimentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataEHora,Tipo,Status,Local,PacienteId,ProfissionalSaudeId,ProntuarioId")] Atendimento atendimento)
        {
            if (id != atendimento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(atendimento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtendimentoExists(atendimento.Id))
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
            // Se der erro na edição, também repopula os dropdowns corretamente.
            PopulateDropdowns(atendimento.PacienteId, atendimento.ProfissionalSaudeId, atendimento.ProntuarioId);
            return View(atendimento);
        }

        // GET: Atendimentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atendimento = await _context.Atendimentos
                .Include(a => a.Paciente)
                .Include(a => a.ProfissionalSaude)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (atendimento == null)
            {
                return NotFound();
            }

            return View(atendimento);
        }

        // POST: Atendimentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var atendimento = await _context.Atendimentos.FindAsync(id);
            if (atendimento != null)
            {
                _context.Atendimentos.Remove(atendimento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropdowns(object? selectedPaciente = null, object? selectedProfissional = null, object? selectedProntuario = null)
        {
            // Dropdown de Pacientes
            var pacientes = _context.Pacientes.OrderBy(p => p.NomeCompleto).ToList();
            ViewData["PacienteId"] = new SelectList(pacientes, "Id", "NomeCompleto", selectedPaciente);

            // Dropdown de Profissionais de Saúde
            var profissionais = _context.ProfissionaisSaude.OrderBy(p => p.NomeCompleto).ToList();
            ViewData["ProfissionalSaudeId"] = new SelectList(profissionais, "Id", "NomeCompleto", selectedProfissional);

            // Dropdown de Prontuários (com texto descritivo)
            var prontuarios = _context.Prontuarios.Include(p => p.Paciente).ToList();
            var listaProntuarios = prontuarios.Select(p => new {
                Value = p.NumeroProntuario,
                Text = $"Nº {p.NumeroProntuario} (Paciente: {p.Paciente?.NomeCompleto ?? "N/A"})"
            });
            ViewData["ProntuarioId"] = new SelectList(listaProntuarios, "Value", "Text", selectedProntuario);
        }

        private bool AtendimentoExists(int id)
        {
            return _context.Atendimentos.Any(e => e.Id == id);
        }
    }
}
