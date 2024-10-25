using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ContactFormsController : Controller
    {
        private readonly AppDbContext _context;

        public ContactFormsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ContactForms
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContactForms.ToListAsync());
        }

        // GET: ContactForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactForm = await _context.ContactForms
                .FirstOrDefaultAsync(m => m.ContactFormId == id);
            if (contactForm == null)
            {
                return NotFound();
            }

            return View(contactForm);
        }

        // GET: ContactForms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactFormId,ClintPhone,ClintMessage,ClintEmail,IsDeleted,IsActive,CreationDate,UpdateDate")] ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactForm);
        }

        // GET: ContactForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactForm = await _context.ContactForms.FindAsync(id);
            if (contactForm == null)
            {
                return NotFound();
            }
            return View(contactForm);
        }

        // POST: ContactForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactFormId,ClintPhone,ClintMessage,ClintEmail,IsDeleted,IsActive,CreationDate,UpdateDate")] ContactForm contactForm)
        {
            if (id != contactForm.ContactFormId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactFormExists(contactForm.ContactFormId))
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
            return View(contactForm);
        }

        // GET: ContactForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactForm = await _context.ContactForms
                .FirstOrDefaultAsync(m => m.ContactFormId == id);
            if (contactForm == null)
            {
                return NotFound();
            }

            return View(contactForm);
        }

        // POST: ContactForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactForm = await _context.ContactForms.FindAsync(id);
            if (contactForm != null)
            {
                _context.ContactForms.Remove(contactForm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactFormExists(int id)
        {
            return _context.ContactForms.Any(e => e.ContactFormId == id);
        }
    }
}
