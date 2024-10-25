
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;



namespace WebApplication1.Models.ViewComponents
{

    public class ArticleViewComponent : ViewComponent
    {
        private AppDbContext db;
        public ArticleViewComponent(AppDbContext _db)
        {

            db = _db;

        }
        public IViewComponentResult Invoke()
        {
            return View(db.Articles.ToList());
        }
    }
}
