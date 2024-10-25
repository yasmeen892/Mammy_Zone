using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;



namespace WebApplication1.Models.ViewComponents

{
    public class EventViewComponent : ViewComponent
    {
        private AppDbContext db;
        public EventViewComponent(AppDbContext _db)
        {

            db = _db;

        }
        public IViewComponentResult Invoke()
        {
            return View(db.Events.ToList());
        }
    }
}
