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
    public class ExamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Exames
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Exames
                .Include(e => e.Atendimento.Paciente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Exames/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var exame = await _context.Exames
                .Include(e => e.Atendimento)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (exame == null)
                return NotFound();

            return View(exame);
        }

        // GET: Exames/Create
        public IActionResult Create()
        {
            PopulateDropdown();
            return View();
        }

        // POST: Exames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoExame,DataSolicitacao,DataRealizacao,Resultado,AtendimentoId")] Exame exame)
        {
            if (ModelState.IsValid)
            {
                exame.Id = Guid.NewGuid();
                _context.Add(exame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdown(exame.AtendimentoId);
            return View(exame);
        }

        // GET: Exames/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var exame = await _context.Exames.FindAsync(id);

            if (exame == null)
                return NotFound();

            PopulateDropdown(exame.AtendimentoId);
            return View(exame);
        }

        // POST: Exames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,TipoExame,DataSolicitacao,DataRealizacao,Resultado,AtendimentoId")] Exame exame)
        {
            if (id != exame.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExameExists(exame.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdown(exame.AtendimentoId);
            return View(exame);
        }

        // GET: Exames/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exame = await _context.Exames
                .Include(e => e.Atendimento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exame == null)
            {
                return NotFound();
            }

            return View(exame);
        }

        // POST: Exames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var exame = await _context.Exames.FindAsync(id);
            if (exame != null)
            {
                _context.Exames.Remove(exame);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropdown(object? selectedAtendimento = null)
        {
            var atendimentos = _context.Atendimentos
                                       .Include(a => a.Paciente)
                                       .OrderByDescending(a => a.DataEHora)
                                       .ToList();
            var listaAtendimentos = atendimentos.Select(a => new {
                Value = a.Id,
                Text = $"ID: {a.Id} (Paciente: {a.Paciente?.NomeCompleto ?? "N/A"})"
            });
            ViewData["AtendimentoId"] = new SelectList(listaAtendimentos, "Value", "Text", selectedAtendimento);
        }

        private bool ExameExists(Guid id)
        {
            return _context.Exames.Any(e => e.Id == id);
        }
    }
}
