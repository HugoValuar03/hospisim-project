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
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Atendimentos.Include(a => a.Paciente).Include(a => a.ProfissionalSaude).Include(a => a.Prontuario);
            return View(await applicationDbContext.ToListAsync());
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
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Cpf");
            ViewData["ProfissionalSaudeId"] = new SelectList(_context.ProfissionaisSaude, "Id", "Cpf");
            ViewData["ProntuarioId"] = new SelectList(_context.Prontuarios, "NumeroProntuario", "NumeroProntuario");
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
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Cpf", atendimento.PacienteId);
            ViewData["ProfissionalSaudeId"] = new SelectList(_context.ProfissionaisSaude, "Id", "Cpf", atendimento.ProfissionalSaudeId);
            ViewData["ProntuarioId"] = new SelectList(_context.Prontuarios, "NumeroProntuario", "NumeroProntuario", atendimento.ProntuarioId);
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
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Cpf", atendimento.PacienteId);
            ViewData["ProfissionalSaudeId"] = new SelectList(_context.ProfissionaisSaude, "Id", "Cpf", atendimento.ProfissionalSaudeId);
            ViewData["ProntuarioId"] = new SelectList(_context.Prontuarios, "NumeroProntuario", "NumeroProntuario", atendimento.ProntuarioId);
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
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Cpf", atendimento.PacienteId);
            ViewData["ProfissionalSaudeId"] = new SelectList(_context.ProfissionaisSaude, "Id", "Cpf", atendimento.ProfissionalSaudeId);
            ViewData["ProntuarioId"] = new SelectList(_context.Prontuarios, "NumeroProntuario", "NumeroProntuario", atendimento.ProntuarioId);
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
                .Include(a => a.Prontuario)
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

        private bool AtendimentoExists(int id)
        {
            return _context.Atendimentos.Any(e => e.Id == id);
        }
    }
}
