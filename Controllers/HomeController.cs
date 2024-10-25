
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext db;
        public HomeController(AppDbContext _db)
        {

            db = _db;
        }


        public IActionResult Index()
        {
            return View();
        }


    
        public IActionResult ArticleDetailes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var data = db.Articles.Find(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }

            return View(data);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
