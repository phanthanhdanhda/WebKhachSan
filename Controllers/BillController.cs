using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCK.Data;
using WebCK.Repositories;

namespace WebCK.Controllers
{
    public class BillController : Controller
    {
        private readonly IFormRepository _formRepository;
        private readonly IBillRepository _billRepository;
        private readonly ApplicationDbContext _context;
        public BillController(IBillRepository billRepository, ApplicationDbContext context,
            IFormRepository formRepository)
        {
            _formRepository = formRepository;
            _billRepository = billRepository;
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var bills = await _billRepository.GetAllAsync();
            return View(bills);
        }
        public async Task<IActionResult> Display(int id)
        {
            var bill = await _billRepository.GetByIdAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            if (bill.FormId.HasValue)
            {
                int formId = bill.FormId.Value;
                var form = await _formRepository.GetByIdAsync(formId);
                ViewBag.Form = form;
            }

            return View(bill);
        }
    }
}
