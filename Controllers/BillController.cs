using Microsoft.AspNetCore.Mvc;

namespace WebCK.Controllers
{
    public class BillController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
