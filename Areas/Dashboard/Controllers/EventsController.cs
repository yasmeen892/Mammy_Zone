using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.Dashboard.Controllers
{
	[Area("Dashboard")]
	public class EventsController : Controller
	{
		private readonly AppDbContext _context;

		public EventsController(AppDbContext context)
		{
			_context = context;
		}

		// GET: Dashboard/Events
		public async Task<IActionResult> Index()
		{
			return View(await _context.Events.ToListAsync());
		}

		// GET: Dashboard/Events/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var @event = await _context.Events
				.FirstOrDefaultAsync(m => m.EventId == id);
			if (@event == null)
			{
				return NotFound();
			}

			return View(@event);
		}

		// GET: Dashboard/Events/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Dashboard/Events/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Event @event)
		{
			if (ModelState.IsValid)
			{
				if (@event.ImageFile != null && @event.ImageFile.Length > 0)
				{
					// تحديد المسار حيث ستخزن الصورة
					var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
					var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(@event.ImageFile.FileName);
					var filePath = Path.Combine(uploadsFolder, fileName);

					// التحقق من وجود المجلد، وإنشاؤه إذا لم يكن موجودًا
					if (!Directory.Exists(uploadsFolder))
					{
						Directory.CreateDirectory(uploadsFolder);
					}

					// نسخ الملف إلى المسار المحدد
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await @event.ImageFile.CopyToAsync(fileStream);
					}

					// حفظ مسار الصورة في قاعدة البيانات
					@event.Image = $"/images/{fileName}";
				}

				// إضافة الفعالية إلى قاعدة البيانات
				_context.Events.Add(@event);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			return View(@event);
		}

		// GET: Dashboard/Events/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var @event = await _context.Events.FindAsync(id);
			if (@event == null)
			{
				return NotFound();
			}
			return View(@event);
		}

		// POST: Dashboard/Events/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Event @event)
		{
			if (id != @event.EventId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				if (@event.ImageFile != null && @event.ImageFile.Length > 0)
				{
					// تحديد المسار حيث ستخزن الصورة الجديدة
					var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
					var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(@event.ImageFile.FileName);
					var filePath = Path.Combine(uploadsFolder, fileName);

					// التحقق من وجود المجلد، وإنشاؤه إذا لم يكن موجودًا
					if (!Directory.Exists(uploadsFolder))
					{
						Directory.CreateDirectory(uploadsFolder);
					}

					// نسخ الملف إلى المسار المحدد
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await @event.ImageFile.CopyToAsync(fileStream);
					}

					// حفظ مسار الصورة الجديدة في قاعدة البيانات
					@event.Image = $"/images/{fileName}";
				}

				try
				{
					_context.Update(@event);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!EventExists(@event.EventId))
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
			return View(@event);
		}

		// GET: Dashboard/Events/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var @event = await _context.Events
				.FirstOrDefaultAsync(m => m.EventId == id);
			if (@event == null)
			{
				return NotFound();
			}

			return View(@event);
		}

		// POST: Dashboard/Events/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var @event = await _context.Events.FindAsync(id);
			if (@event != null)
			{
				_context.Events.Remove(@event);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool EventExists(int id)
		{
			return _context.Events.Any(e => e.EventId == id);
		}
	}
}
