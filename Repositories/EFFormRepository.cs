using Microsoft.EntityFrameworkCore;
using WebCK.Data;
using WebCK.Models;

namespace WebCK.Repositories
{
    public class EFFormRepository : IFormRepository
    {
        private readonly ApplicationDbContext _context;
        public EFFormRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BookingForm>> GetAllAsync()
        {
            // return await _context.Forms.ToListAsync();
            return await _context.Forms.ToListAsync();
        }
        public async Task<BookingForm> GetByIdAsync(int id)
        {
            // return await _context.Forms.FindAsync(id);
            return await _context.Forms.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(BookingForm BookingForm)
        {
            _context.Forms.Add(BookingForm);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(BookingForm BookingForm)
        {
            _context.Forms.Update(BookingForm);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var form = await _context.Forms.FindAsync(id);
            _context.Forms.Remove(form);
            await _context.SaveChangesAsync();
        }
    }
}
