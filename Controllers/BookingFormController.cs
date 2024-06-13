using Microsoft.AspNetCore.Authorization;
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
		private readonly IBillRepository _billRepository;
		private readonly ApplicationDbContext _context;
        public BookingFormController(IRoomRepository roomRepository, IFormRepository formRepository,
            IBillRepository billRepository, ApplicationDbContext context) 
        {  
            _roomRepository = roomRepository;
            _formRepository = formRepository;
            _billRepository = billRepository;
            _context = context;
        }
        [Authorize(Roles = "Admin")]
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
            ViewBag.Deposit = Math.Round((float)room.Price / 5,0);
            ViewBag.RoomId = roomId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(int roomId, BookingForm form)
        {
            var room = await _roomRepository.GetByIdAsync(roomId);
            if (room == null)
                return NotFound();
            form.RoomId = room.Id;
            form.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                await _formRepository.AddAsync(form);
                return RedirectToAction("Display", new { id = form.Id });
            }
            return View(form);
        }
		//[HttpPost]
		//public async Task<ActionResult> CalculateRent(int roomId, string checkInDate, string checkOutDate)
		//{
		//	DateTime checkIn = DateTime.Parse(checkInDate);
		//	DateTime checkOut = DateTime.Parse(checkOutDate);
            //var  room = await _roomRepository.GetByIdAsync(roomId);
		//	int numberOfDays = (int)(checkOut - checkIn).TotalDays;

		//	// Tính giá thuê và tiền đặt cọc tại đây
		//	double totalRentPrice = numberOfDays * room.Price;
		//	double depositAmount = Math.Round(totalRentPrice * 0.2);

		//	return Json(new { bookingFee = totalRentPrice, deposit = depositAmount });
		//}
		public async Task<IActionResult> Display(int id)
        {
            var form = await _formRepository.GetByIdAsync(id);
            if (form == null)
            {
                return NotFound();
            }
            return View(form);
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _formRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> ThanhToan(int formId)
        {
            BookingForm form = await _formRepository.GetByIdAsync(formId);
			ViewBag.Form = form;
			if (form == null)
            {
                return NotFound();
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ThanhToan(int formId, DepositBill bill)
        {
            var form = await _formRepository.GetByIdAsync(formId);
            ViewBag.Form = form;
            if (form == null)
            {
                return NotFound();
            }
            bill.UserId = form.UserId;
            bill.FormId = form.Id;
            bill.TotalFee = form.Deposit;
            
            if (ModelState.IsValid)
            {
				await _billRepository.AddAsync(bill);
				return RedirectToAction("Display", "Bill", new { id = bill.Id });
            }
            return View();
        }
    }
}
