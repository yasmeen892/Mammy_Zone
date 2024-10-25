using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.AspNetCore.Hosting; // For _hostEnvironment

namespace WebApplication1.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class WorkShopsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public WorkShopsController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Dashboard/WorkShops
        public async Task<IActionResult> Index()
        {
            ViewBag.SpecialistId = new SelectList(_context.Specialists, "SpecialistId", "SpecialistName");
            var appDbContext = _context.WorkShops.Include(w => w.Specialist);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Dashboard/WorkShops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workShop = await _context.WorkShops
                .Include(w => w.Specialist)
                .FirstOrDefaultAsync(m => m.WorkShopId == id);
            if (workShop == null)
            {
                return NotFound();
            }

            return View(workShop);
        }

        // GET: Dashboard/WorkShops/Create
        public IActionResult Create()
        {
            ViewData["SpecialistId"] = new SelectList(_context.Specialists, "SpecialistId", "SpecialistName");
            return View();
        }

        // POST: Dashboard/WorkShops/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkShop workShop, IFormFile ImageFile, IFormFile VideoFile)
        {
            if (ModelState.IsValid)
            {
                // Handle file uploads
                await SaveUploadedFilesAsync(workShop, ImageFile, VideoFile);

                _context.Add(workShop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SpecialistId"] = new SelectList(_context.Specialists, "SpecialistId", "SpecialistName", workShop.SpecialistId);
            return View(workShop);
        }

        // GET: Dashboard/WorkShops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workShop = await _context.WorkShops.FindAsync(id);
            if (workShop == null)
            {
                return NotFound();
            }

            ViewData["SpecialistId"] = new SelectList(_context.Specialists, "SpecialistId", "SpecialistName", workShop.SpecialistId);
            return View(workShop);
        }

        // POST: Dashboard/WorkShops/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WorkShop workShop, IFormFile ImageFile, IFormFile VideoFile)
        {
            if (id != workShop.WorkShopId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Handle file uploads
                    await SaveUploadedFilesAsync(workShop, ImageFile, VideoFile);

                    _context.Update(workShop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkShopExists(workShop.WorkShopId))
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

            ViewData["SpecialistId"] = new SelectList(_context.Specialists, "SpecialistId", "SpecialistName", workShop.SpecialistId);
            return View(workShop);
        }

        // GET: Dashboard/WorkShops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workShop = await _context.WorkShops
                .Include(w => w.Specialist)
                .FirstOrDefaultAsync(m => m.WorkShopId == id);
            if (workShop == null)
            {
                return NotFound();
            }

            return View(workShop);
        }

        // POST: Dashboard/WorkShops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workShop = await _context.WorkShops.FindAsync(id);
            if (workShop != null)
            {
                _context.WorkShops.Remove(workShop);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkShopExists(int id)
        {
            return _context.WorkShops.Any(e => e.WorkShopId == id);
        }

        // Helper method to handle file uploads
        private async Task SaveUploadedFilesAsync(WorkShop workShop, IFormFile ImageFile, IFormFile VideoFile)
        {
            // Handle image upload
            if (ImageFile != null)
            {
                var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "uploads/Videos", ImageFile.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }
                workShop.Image = "/uploads/Videos/" + ImageFile.FileName;
            }

            // Handle video upload
            if (VideoFile != null)
            {
                var videoPath = Path.Combine(_hostEnvironment.WebRootPath, "uploads/Videos", VideoFile.FileName);
                using (var stream = new FileStream(videoPath, FileMode.Create))
                {
                    await VideoFile.CopyToAsync(stream);
                }
                workShop.IntroVideoPath = "/uploads/Videos/" + VideoFile.FileName;
            }
        }
    }
}
