using System.Threading.Tasks;
using TourManagementSystem.Models;

namespace TourManagementSystem.Services
{
    public interface IAuthService
    {
        Task<string> GenerateJwtToken(User user);
    }
}
