using WebCK.Models;

namespace WebCK.Repositories
{
	public interface IBillRepository
	{
		Task<IEnumerable<DepositBill>> GetAllAsync();
		Task<DepositBill> GetByIdAsync(int id);
		Task AddAsync(DepositBill bill);
		Task UpdateAsync(DepositBill bill);
		Task DeleteAsync(int id);
	}
}
