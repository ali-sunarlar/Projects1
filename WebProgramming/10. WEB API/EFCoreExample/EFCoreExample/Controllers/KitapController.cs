﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFCoreExample.Models;

namespace EFCoreExample.Controllers
{
    public class KitapController : Controller
    {
        private readonly KitaplikContext _context =new KitaplikContext();

        // GET: Kitap
        public async Task<IActionResult> Index()
        {
            var kitaplikContext = _context.Kitaplar.Include(k => k.Yazar); //entity framework default olarak asenkron çalıştırır
            return View(await kitaplikContext.ToListAsync());
        }

        // GET: Kitap/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitap = await _context.Kitaplar
                .Include(k => k.Yazar)
                .FirstOrDefaultAsync(m => m.KitapID == id);
            if (kitap == null)
            {
                return NotFound();
            }

            return View(kitap);
        }

        // GET: Kitap/Create
        public IActionResult Create()
        {
            ViewData["YazarID"] = new SelectList(_context.Yazarlar, "YazarID", "YazarAd");
            return View();
        }

        // POST: Kitap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KitapID,KitapAdi,YazarID")] Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kitap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["YazarID"] = new SelectList(_context.Yazarlar, "YazarID", "YazarAd", kitap.YazarID);
            return View(kitap);
        }

        // GET: Kitap/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitap = await _context.Kitaplar.FindAsync(id);
            if (kitap == null)
            {
                return NotFound();
            }
            ViewData["YazarID"] = new SelectList(_context.Yazarlar, "YazarID", "YazarAd", kitap.YazarID);
            return View(kitap);
        }

        // POST: Kitap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KitapID,KitapAdi,YazarID")] Kitap kitap)
        {
            if (id != kitap.KitapID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kitap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KitapExists(kitap.KitapID))
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
            ViewData["YazarID"] = new SelectList(_context.Yazarlar, "YazarID", "YazarAd", kitap.YazarID);
            return View(kitap);
        }

        // GET: Kitap/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitap = await _context.Kitaplar
                .Include(k => k.Yazar)
                .FirstOrDefaultAsync(m => m.KitapID == id);
            if (kitap == null)
            {
                return NotFound();
            }

            return View(kitap);
        }

        // POST: Kitap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kitap = await _context.Kitaplar.FindAsync(id);
            if (kitap != null)
            {
                _context.Kitaplar.Remove(kitap);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KitapExists(int id)
        {
            return _context.Kitaplar.Any(e => e.KitapID == id);
        }
    }
}
