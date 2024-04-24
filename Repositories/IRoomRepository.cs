using WebCK.Models;

namespace WebCK.Repositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room> GetByIdAsync(int id);
        Task AddAsync(Room product);
        Task UpdateAsync(Room room);
        Task DeleteAsync(int id);
    }
}
