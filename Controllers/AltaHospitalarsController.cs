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
    public class AltaHospitalarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AltaHospitalarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AltaHospitalars
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var altas = _context.AltasHospitalares
                .Include(a => a.Internacao.Paciente)
                .AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                altas = altas.Where(a => a.Internacao.Paciente.NomeCompleto.Contains(searchString));
            }

            return View(await altas.OrderByDescending(a => a.Data).ToListAsync());
        }

        // GET: AltaHospitalars/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            // Adicionado .ThenInclude() para carregar os dados do Paciente
            var altaHospitalar = await _context.AltasHospitalares
                .Include(a => a.Internacao)
                    .ThenInclude(i => i.Paciente)
                .FirstOrDefaultAsync(m => m.InternacaoId == id);

            if (altaHospitalar == null) return NotFound();

            return View(altaHospitalar);
        }

        // GET: AltaHospitalars/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST: AltaHospitalars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InternacaoId,Data,CondicaoPaciente,InstrucoesPosAlta")] AltaHospitalar altaHospitalar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(altaHospitalar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns(altaHospitalar.InternacaoId);
            return View(altaHospitalar);
        }

        // GET: AltaHospitalars/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var altaHospitalar = await _context.AltasHospitalares.FindAsync(id);
            if (altaHospitalar == null) return NotFound();

            PopulateDropdowns(altaHospitalar.InternacaoId);
            return View(altaHospitalar);
        }

        // POST: AltaHospitalars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("InternacaoId,Data,CondicaoPaciente,InstrucoesPosAlta")] AltaHospitalar altaHospitalar)
        {
            if (id != altaHospitalar.InternacaoId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(altaHospitalar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AltaHospitalarExists(altaHospitalar.InternacaoId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdowns(altaHospitalar.InternacaoId);
            return View(altaHospitalar);
        }

        // GET: AltaHospitalars/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            // Adicionado .ThenInclude() para carregar os dados do Paciente
            var altaHospitalar = await _context.AltasHospitalares
                .Include(a => a.Internacao)
                    .ThenInclude(i => i.Paciente)
                .FirstOrDefaultAsync(m => m.InternacaoId == id);

            if (altaHospitalar == null)
            {
                return NotFound();
            }

            return View(altaHospitalar);
        }

        // POST: AltaHospitalars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var altaHospitalar = await _context.AltasHospitalares.FindAsync(id);
            if (altaHospitalar != null)
            {
                _context.AltasHospitalares.Remove(altaHospitalar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropdowns(object? selectedInternacao = null)
        {
            var internacoes = _context.Internacoes
                                      .Where(i => i.AltaHospitalar == null) // Pega internações que não tem alta
                                      .Where(i => i.StatusInternacao == StatusInternacao.ALTA_CONCEDIDA) // E tem que ter o status "Alta Concedida"
                                      .Include(i => i.Paciente)
                                      .OrderByDescending(i => i.DataEntrada)
                                      .ToList();

            var listaInternacoes = internacoes.Select(i => new {
                Value = i.Id,
                Text = $"Paciente: {i.Paciente?.NomeCompleto ?? "N/A"} (Entrada: {i.DataEntrada.ToShortDateString()})"
            });

            ViewData["InternacaoId"] = new SelectList(listaInternacoes, "Value", "Text", selectedInternacao);
        }

        private bool AltaHospitalarExists(Guid id)
        {
            return _context.AltasHospitalares.Any(e => e.InternacaoId == id);
        }
    }
}
