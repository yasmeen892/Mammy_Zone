
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;


namespace WebApplication1.Models.ViewComponents
{
    public class ConectformViewComponent : ViewComponent
    {
        private AppDbContext db;
        public ConectformViewComponent(AppDbContext _db)
        {

            db = _db;

        }
        public IViewComponentResult Invoke()
        {
            return View(db.ContactForms.ToList());
        }
    }
}


