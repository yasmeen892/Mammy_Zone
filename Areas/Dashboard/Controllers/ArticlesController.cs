using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.Dashboard.Controllers
{
	[Area("Dashboard")]
	public class ArticlesController : Controller
	{
		private readonly AppDbContext _context;

		public ArticlesController(AppDbContext context)
		{
			_context = context;
		}

		// GET: Dashboard/Articles
		public async Task<IActionResult> Index()
		{
			return View(await _context.Articles.ToListAsync());
		}

		// GET: Dashboard/Articles/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var article = await _context.Articles
				.FirstOrDefaultAsync(m => m.ArticleId == id);
			if (article == null)
			{
				return NotFound();
			}

			return View(article);
		}

		// GET: Dashboard/Articles/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Dashboard/Articles/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create( Article article, IFormFile ImageFile)
		{
			if (ModelState.IsValid)
			{
				// Handle image upload
				if (ImageFile != null)
				{
					var imagePath = Path.Combine("wwwroot/uploads/images", ImageFile.FileName);
					using (var stream = new FileStream(imagePath, FileMode.Create))
					{
						await ImageFile.CopyToAsync(stream);
					}
					article.Image = "/uploads/images/" + ImageFile.FileName;
				}

				_context.Add(article);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(article);
		}

		// GET: Dashboard/Articles/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var article = await _context.Articles.FindAsync(id);
			if (article == null)
			{
				return NotFound();
			}
			return View(article);
		}

		// POST: Dashboard/Articles/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ArticleId,ArticleTitle,ArticleContent,Category,IsDeleted,IsActive,CreationDate,UpdateDate")] Article article, IFormFile ImageFile)
		{
			if (id != article.ArticleId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					// Handle image upload
					if (ImageFile != null)
					{
						var imagePath = Path.Combine("wwwroot/uploads/images", ImageFile.FileName);
						using (var stream = new FileStream(imagePath, FileMode.Create))
						{
							await ImageFile.CopyToAsync(stream);
						}
						article.Image = "/uploads/images/" + ImageFile.FileName;
					}

					_context.Update(article);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ArticleExists(article.ArticleId))
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
			return View(article);
		}

		// GET: Dashboard/Articles/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var article = await _context.Articles
				.FirstOrDefaultAsync(m => m.ArticleId == id);
			if (article == null)
			{
				return NotFound();
			}

			return View(article);
		}

		// POST: Dashboard/Articles/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var article = await _context.Articles.FindAsync(id);
			if (article != null)
			{
				_context.Articles.Remove(article);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ArticleExists(int id)
		{
			return _context.Articles.Any(e => e.ArticleId == id);
		}
	}
}
