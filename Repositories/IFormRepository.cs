using WebCK.Models;

namespace WebCK.Repositories
{
    public interface IFormRepository
    {
        Task<IEnumerable<BookingForm>> GetAllAsync();
        Task<BookingForm> GetByIdAsync(int id);
        Task AddAsync(BookingForm form);
        Task UpdateAsync(BookingForm form);
    }
}
