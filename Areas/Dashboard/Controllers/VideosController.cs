using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class VideosController : Controller
    {
        private readonly AppDbContext _context;

        public VideosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/Videos
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Videoes.Include(v => v.WorkShop);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Dashboard/Videos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Videoes
                .Include(v => v.WorkShop)
                .FirstOrDefaultAsync(m => m.VideoId == id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // GET: Dashboard/Videos/Create
        public IActionResult Create()
        {
            ViewData["WorkShopId"] = new SelectList(_context.WorkShops, "WorkShopId", "Category");
            return View();
        }

        // POST: Dashboard/Videos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VideoId,VideoPath,Title,WorkShopId")] Video video)
        {
            if (ModelState.IsValid)
            {
                _context.Add(video);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkShopId"] = new SelectList(_context.WorkShops, "WorkShopId", "Category", video.WorkShopId);
            return View(video);
        }

        // GET: Dashboard/Videos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Videoes.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }
            ViewData["WorkShopId"] = new SelectList(_context.WorkShops, "WorkShopId", "Category", video.WorkShopId);
            return View(video);
        }

        // POST: Dashboard/Videos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VideoId,VideoPath,Title,WorkShopId")] Video video)
        {
            if (id != video.VideoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(video);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoExists(video.VideoId))
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
            ViewData["WorkShopId"] = new SelectList(_context.WorkShops, "WorkShopId", "Category", video.WorkShopId);
            return View(video);
        }

        // GET: Dashboard/Videos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Videoes
                .Include(v => v.WorkShop)
                .FirstOrDefaultAsync(m => m.VideoId == id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // POST: Dashboard/Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var video = await _context.Videoes.FindAsync(id);
            if (video != null)
            {
                _context.Videoes.Remove(video);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoExists(int id)
        {
            return _context.Videoes.Any(e => e.VideoId == id);
        }
    }
}
