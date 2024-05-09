using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebCK.Data;
using WebCK.Models;
using WebCK.Repositories;

namespace WebCK.Controllers
{
    public class BookingFormController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IFormRepository _formRepository;
        private readonly ApplicationDbContext _context;
        public BookingFormController(IRoomRepository roomRepository, IFormRepository formRepository,
            ApplicationDbContext context) 
        {  
            _roomRepository = roomRepository;
            _formRepository = formRepository;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var forms = await _formRepository.GetAllAsync();
            return View(forms);
        }
        [HttpGet]
        public async Task<IActionResult> Add(int roomId)
        {
            var room = await _roomRepository.GetByIdAsync(roomId);
            ViewBag.RoomPrice = room.Price;
            ViewBag.Deposit = room.Price / 5;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(int roomId, BookingForm form)
        {
            form.RoomId = roomId;
            form.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                await _formRepository.AddAsync(form);
                return RedirectToAction("Display", new { id = form.Id });
            }
            return View(form);
        }
        public async Task<IActionResult> Display(int id)
        {
            var form = await _formRepository.GetByIdAsync(id);
            ViewBag.Name = form.FullName;
            if (form == null)
            {
                return NotFound();
            }
            return View(form);
        }
        //[HttpGet]
        //public async Task<IActionResult> ThanhToan(int formId)
        //{
        //    var form = await _formRepository.GetByIdAsync(formId);
        //    ViewBag.Form = form;
        //    var room = await _roomRepository.GetByIdAsync((int)form.RoomId);
        //    ViewBag.Room = room;
        //    return View();
        //}
        //[HttpPost]
        public async Task<IActionResult> ThanhToan(int formId, DepositBill bill)
        {
            var form = await _formRepository.GetByIdAsync(formId);
            ViewBag.Form = form;
            if(form != null)
            {
                bill.UserId = form.UserId;
                bill.FormId = form.Id;
                bill.TotalFee = form.Deposit;
            }
            if (ModelState.IsValid)
            {
                _context.Bills.Add(bill);
                await _context.SaveChangesAsync(); // Use async version of SaveChanges

                return View(bill);
            }
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var form = await _formRepository.GetByIdAsync(id);
            if (form == null)
            {
                return NotFound();
            }
            return View(form);
        }
        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roomRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> PlaceOrder(int formId)
        {
            BookingForm form = await _formRepository.GetByIdAsync(formId);
            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }
    }
}
