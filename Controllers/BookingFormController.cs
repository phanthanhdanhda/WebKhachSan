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
        public BookingFormController(IRoomRepository roomRepository, IFormRepository formRepository) 
        {  
            _roomRepository = roomRepository;
            _formRepository = formRepository;
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
            if (form == null)
            {
                return NotFound();
            }
            return View(form);
        }
    }
}
