using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebCK.Data;
using WebCK.Models;

namespace WebCK.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {

            var smallroom = _context.Rooms.Where(p => p.Id < 7).ToList();
            return View(smallroom);
        }

        public IActionResult Viewmore() 
        {
            var room = _context.Rooms.ToList();
            return View(room);
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
        public IActionResult Details(int id) {
            
            return View();
        }
    }
}
