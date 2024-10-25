using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class SpecialistsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly string _uploadPath;

        public SpecialistsController(AppDbContext context)
        {
            _context = context;
            _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        }

        // GET: Dashboard/Specialists
        public async Task<IActionResult> Index()
        {
            return View(await _context.Specialists.ToListAsync());
        }

        // GET: Dashboard/Specialists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialist = await _context.Specialists
                .FirstOrDefaultAsync(m => m.SpecialistId == id);
            if (specialist == null)
            {
                return NotFound();
            }

            return View(specialist);
        }

        // GET: Dashboard/Specialists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Specialists/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Specialist specialist, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                // Handle image upload
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                    var filePath = Path.Combine(_uploadPath, fileName);

                    // Save the uploaded file to the server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    specialist.Image = fileName; // Save the file name to the database
                }

                _context.Add(specialist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialist);
        }

        // GET: Dashboard/Specialists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialist = await _context.Specialists.FindAsync(id);
            if (specialist == null)
            {
                return NotFound();
            }
            return View(specialist);
        }

        // POST: Dashboard/Specialists/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Specialist specialist, IFormFile ImageFile)
        {
            if (id != specialist.SpecialistId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Handle image upload during edit
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                        var filePath = Path.Combine(_uploadPath, fileName);

                        // Save the new uploaded file to the server
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(stream);
                        }

                        specialist.Image = fileName; // Update the file name in the database
                    }

                    _context.Update(specialist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialistExists(specialist.SpecialistId))
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
            return View(specialist);
        }

        // GET: Dashboard/Specialists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialist = await _context.Specialists
                .FirstOrDefaultAsync(m => m.SpecialistId == id);
            if (specialist == null)
            {
                return NotFound();
            }

            return View(specialist);
        }

        // POST: Dashboard/Specialists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialist = await _context.Specialists.FindAsync(id);
            if (specialist != null)
            {
                _context.Specialists.Remove(specialist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialistExists(int id)
        {
            return _context.Specialists.Any(e => e.SpecialistId == id);
        }
    }
}
