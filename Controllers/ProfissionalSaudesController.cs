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
    public class ProfissionalSaudesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfissionalSaudesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProfissionalSaudes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProfissionaisSaude.ToListAsync());
        }

        // GET: ProfissionalSaudes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profissionalSaude = await _context.ProfissionaisSaude
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profissionalSaude == null)
            {
                return NotFound();
            }

            return View(profissionalSaude);
        }

        // GET: ProfissionalSaudes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProfissionalSaudes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeCompleto,Cpf,Telefone,RegistroConselho,TipoRegistro,DataAdmissao,CargaHorarioSemanal,Turno,Ativo")] ProfissionalSaude profissionalSaude)
        {
            if (ModelState.IsValid)
            {
                profissionalSaude.Id = Guid.NewGuid();
                _context.Add(profissionalSaude);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profissionalSaude);
        }

        // GET: ProfissionalSaudes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profissionalSaude = await _context.ProfissionaisSaude.FindAsync(id);
            if (profissionalSaude == null)
            {
                return NotFound();
            }
            return View(profissionalSaude);
        }

        // POST: ProfissionalSaudes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,NomeCompleto,Cpf,Telefone,RegistroConselho,TipoRegistro,DataAdmissao,CargaHorarioSemanal,Turno,Ativo")] ProfissionalSaude profissionalSaude)
        {
            if (id != profissionalSaude.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profissionalSaude);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfissionalSaudeExists(profissionalSaude.Id))
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
            return View(profissionalSaude);
        }

        // GET: ProfissionalSaudes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profissionalSaude = await _context.ProfissionaisSaude
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profissionalSaude == null)
            {
                return NotFound();
            }

            return View(profissionalSaude);
        }

        // POST: ProfissionalSaudes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var profissionalSaude = await _context.ProfissionaisSaude.FindAsync(id);
            if (profissionalSaude != null)
            {
                _context.ProfissionaisSaude.Remove(profissionalSaude);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfissionalSaudeExists(Guid id)
        {
            return _context.ProfissionaisSaude.Any(e => e.Id == id);
        }
    }
}
