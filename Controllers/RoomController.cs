using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebCK.Models;
using WebCK.Repositories;

namespace WebCK.Controllers
{
    [Authorize(Roles = "Admin")]

    public class RoomController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly ICategoryRepository _categoryRepository;
        public RoomController(IRoomRepository roomRepository,
        ICategoryRepository categoryRepository)
        {
            _roomRepository = roomRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            var rooms = await _roomRepository.GetAllAsync();
            return View(rooms);
        }
        // Hiển thị form thêm sản phẩm mới
        public async Task<IActionResult> Add()
        {
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
            return View();
        }
        // Xử lý thêm sản phẩm mới
        [HttpPost]
        public async Task<IActionResult> Add(Room room, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    // Lưu hình ảnh đại diện
                    room.Image = await SaveImage(Image);
                }
                await _roomRepository.AddAsync(room);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
            return View(room);
        }
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/img/rooms", image.FileName);
        using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/img/rooms/" + image.FileName; // Trả về đường dẫn tương đối
        }
        [AllowAnonymous]
        public async Task<IActionResult> Display(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }
        // Hiển thị form cập nhật sản phẩm
        public async Task<IActionResult> Update(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName", room.CategoryId);
            //room.CategoryId);
            return View(room);
        }
        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, Room room, IFormFile Image)
        {
            ModelState.Remove("Image"); // Loại bỏ xác thực ModelState cho Image
            if (id != room.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingroom = await
                _roomRepository.GetByIdAsync(id);
                // Giả định có phương thức GetByIdAsync
                // Giữ nguyên thông tin hình ảnh nếu không có hình mới được tải lên
                if (Image == null)
                {
                    room.Image = existingroom.Image;
                }
                else
                {
                    // Lưu hình ảnh mới
                    room.Image = await SaveImage(Image);
                }
                existingroom.RoomName = room.RoomName;
                existingroom.Price = room.Price;
                existingroom.Description = room.Description;
                existingroom.CategoryId = room.CategoryId;
                existingroom.Image = room.Image;
                await _roomRepository.UpdateAsync(existingroom);
                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
            return View(room);
        }
        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }
        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roomRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}