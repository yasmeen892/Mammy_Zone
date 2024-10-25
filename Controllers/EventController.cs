using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{


    public class EventController : Controller
    {
        private readonly AppDbContext _context;

        public EventController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Details(int id)
        {
            var eventDetails = _context.Events.FirstOrDefault(e => e.EventId == id);
            if (eventDetails == null)
            {
                return NotFound();
            }

            return View(eventDetails);
        }
    }

}
