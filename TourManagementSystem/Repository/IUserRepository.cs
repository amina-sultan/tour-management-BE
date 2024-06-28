using System.Threading.Tasks;
using TourManagementSystem.Models;

namespace TourManagementSystem.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> AddAsync(User user);

    }
}
