using WebApplication1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : Controller
    {
        private AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db=db;
        }
        public IActionResult Index()
        {
			var articles = _db.Articles.ToList();
			var workshops = _db.WorkShops.ToList();
			var specialists = _db.Specialists.ToList();
			var events = _db.Events.ToList();

			// Passing data to the view using ViewBag
			ViewBag.Articles = articles;
			ViewBag.Workshops = workshops;
			ViewBag.Specialists = specialists;
			ViewBag.Events = events;

			return View();


			
            

        }
    }
}
