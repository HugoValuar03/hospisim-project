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
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AltasHospitalares
                .Include(a => a.Internacao)
                    .ThenInclude(i => i.Paciente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AltaHospitalars/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) 
                return NotFound();

            var altaHospitalar = await _context.AltasHospitalares
                .Include(a => a.Internacao.Paciente)
                .FirstOrDefaultAsync(m => m.InternacaoId == id);
            if (altaHospitalar == null) return NotFound();
            return View(altaHospitalar);
        }

        // GET: AltaHospitalars/Create
        public IActionResult Create()
        {
            var internacoes = _context.Internacoes.Include(i => i.Paciente).ToList();

            foreach (var internacao in internacoes)
            {
                if (internacao.Paciente == null)
                {
                    throw new Exception($"PROBLEMA ENCONTRADO! A Internação com ID '{internacao.Id}' tem um PacienteId ('{internacao.PacienteId}') que não corresponde a nenhum paciente existente no banco de dados. Por favor, corrija ou delete este registro de internação problemático.");
                }

                if (string.IsNullOrEmpty(internacao.Paciente.NomeCompleto))
                {
                    throw new Exception($"PROBLEMA ENCONTRADO! O Paciente com ID '{internacao.Paciente.Id}' (associado à Internação ID '{internacao.Id}') está com o campo NomeCompleto vazio ou nulo no banco de dados. Por favor, preencha o nome deste paciente.");
                }
            }

            var listaInternacoes = internacoes.Select(i => new {
                Value = i.Id,
                Text = $"Paciente: {i.Paciente?.NomeCompleto} (Entrada: {i.DataEntrada.ToShortDateString()})"
            });

            ViewData["InternacaoId"] = new SelectList(listaInternacoes, "Value", "Text");

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
            if (id == null)
            {
                return NotFound();
            }

            var altaHospitalar = await _context.AltasHospitalares.FindAsync(id);
            if (altaHospitalar == null)
            {
                return NotFound();
            }
            ViewData["InternacaoId"] = new SelectList(_context.Internacoes, "Id", "Id", altaHospitalar.InternacaoId);
            return View(altaHospitalar);
        }

        // POST: AltaHospitalars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("InternacaoId,Data,CondicaoPaciente,InstrucoesPosAlta")] AltaHospitalar altaHospitalar)
        {
            if (id != altaHospitalar.InternacaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(altaHospitalar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AltaHospitalarExists(altaHospitalar.InternacaoId))
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
            ViewData["InternacaoId"] = new SelectList(_context.Internacoes, "Id", "Id", altaHospitalar.InternacaoId);
            return View(altaHospitalar);
        }

        // GET: AltaHospitalars/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var altaHospitalar = await _context.AltasHospitalares
                .Include(a => a.Internacao)
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
                                .Include(i => i.Paciente)
                                .OrderByDescending(i => i.DataEntrada)
                                .ToList();

            var listaInternacoes = internacoes.Select(i => new
            {
                Value = i.Id,
                Text = $"Paciente: {i.Paciente?.NomeCompleto} - {i.DataEntrada.ToShortDateString()} ({i.StatusInternacao})"
            });
            ViewData["InternacaoId"] = new SelectList(internacoes, "Value", "Text", selectedInternacao);
        }

        private bool AltaHospitalarExists(Guid id)
        {
            return _context.AltasHospitalares.Any(e => e.InternacaoId == id);
        }
    }
}
