
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
namespace WebApplication1.Controllers
{
    public class WorkShopController : Controller
    {
        private readonly AppDbContext _context;

        public WorkShopController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult WDetails(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var data = _context.WorkShops.Find(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }

            return View(data);
        }


        public IActionResult WorkShopVideo(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var data = _context.WorkShops.Find(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }

            return View(data);
        }





    }
}
