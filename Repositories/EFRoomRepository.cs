using Microsoft.EntityFrameworkCore;
using WebCK.Data;
using WebCK.Models;

namespace WebCK.Repositories
{
    public class EFRoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;
        public EFRoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            // return await _context.Rooms.ToListAsync();
            return await _context.Rooms
            .Include(p => p.Category) // Include thông tin về category
            .ToListAsync();
        }
        public async Task<Room> GetByIdAsync(int id)
        {
            // return await _context.Rooms.FindAsync(id);
            // lấy thông tin kèm theo category
            return await _context.Rooms.Include(p =>
            p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(Room Room)
        {
            _context.Rooms.Add(Room);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Room Room)
        {
            _context.Rooms.Update(Room);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var Room = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(Room);
            await _context.SaveChangesAsync();
        }
    }
}