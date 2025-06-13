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
    public class ProntuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProntuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prontuarios
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var prontuarios = from p in _context.Prontuarios
                              .Include(p => p.Paciente)
                              select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                // Filtra pelo nome do paciente ou pelo número do prontuário
                prontuarios = prontuarios.Where(p => p.Paciente.NomeCompleto.Contains(searchString)
                                                  || p.NumeroProntuario.ToString().Contains(searchString));
            }

            return View(await prontuarios.OrderByDescending(p => p.DataAbertura).ToListAsync());
        }

        // GET: Prontuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prontuario = await _context.Prontuarios
                .Include(p => p.Paciente)
                .FirstOrDefaultAsync(m => m.NumeroProntuario == id);
            if (prontuario == null)
            {
                return NotFound();
            }

            return View(prontuario);
        }

        // GET: Prontuarios/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST: Prontuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataAbertura,ObservacoesGerais,PacienteId")] Prontuario prontuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prontuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdowns(prontuario.PacienteId);
            return View(prontuario);
        }

        // GET: Prontuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prontuario = await _context.Prontuarios.FindAsync(id);
            if (prontuario == null)
            {
                return NotFound();
            }
            PopulateDropdowns(prontuario.PacienteId);
            return View(prontuario);
        }

        // POST: Prontuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NumeroProntuario,DataAbertura,ObservacoesGerais,PacienteId")] Prontuario prontuario)
        {
            if (id != prontuario.NumeroProntuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prontuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProntuarioExists(prontuario.NumeroProntuario))
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
            // Em caso de erro, repopula o dropdown da forma correta
            PopulateDropdowns(prontuario.PacienteId);
            return View(prontuario);
        }

        // GET: Prontuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prontuario = await _context.Prontuarios
                .Include(p => p.Paciente)
                .FirstOrDefaultAsync(m => m.NumeroProntuario == id);
            if (prontuario == null)
            {
                return NotFound();
            }

            return View(prontuario);
        }

        // POST: Prontuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prontuario = await _context.Prontuarios.FindAsync(id);
            if (prontuario != null)
            {
                _context.Prontuarios.Remove(prontuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropdowns(object? selectedPaciente = null)
        {
            var pacientes = _context.Pacientes.OrderBy(p => p.NomeCompleto).ToList();
            ViewData["PacienteId"] = new SelectList(pacientes, "Id", "NomeCompleto", selectedPaciente);
        }

        private bool ProntuarioExists(int id)
        {
            return _context.Prontuarios.Any(e => e.NumeroProntuario == id);
        }
    }
}
