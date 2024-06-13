using Microsoft.EntityFrameworkCore;
using WebCK.Data;
using WebCK.Models;

namespace WebCK.Repositories
{
	public class EFBillRepository : IBillRepository
	{
		private readonly ApplicationDbContext _context;
		public EFBillRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<DepositBill>> GetAllAsync()
		{
			// return await _context.Categories.ToListAsync();
			return await _context.Bills.Include(b=>b.BookingForm).ToListAsync();
		}
		public async Task<DepositBill> GetByIdAsync(int id)
		{
			// return await _context.Categories.FindAsync(id);
			// lấy thông tin kèm theo category
			return await _context.Bills.Include(b=>b.BookingForm).FirstOrDefaultAsync(b => b.Id == id);
		}
		public async Task AddAsync(DepositBill bill)
		{
			_context.Bills.Add(bill);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateAsync(DepositBill bill)
		{
			_context.Bills.Update(bill);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteAsync(int id)
		{
			var bill = await _context.Bills.FindAsync(id);
			_context.Bills.Remove(bill);
			await _context.SaveChangesAsync();
		}
	}
}
