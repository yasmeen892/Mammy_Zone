
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;


namespace WebApplication1.Models.ViewComponents
{
    public class SpecialistViewComponent : ViewComponent
    {
        private AppDbContext db;
        public SpecialistViewComponent(AppDbContext _db)
        {

            db = _db;

        }
        public IViewComponentResult Invoke()
        {
            return View(db.Specialists.ToList());
        }
    }
}

