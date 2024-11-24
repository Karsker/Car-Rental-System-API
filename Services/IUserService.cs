using CarRentalSystem.Models;

namespace CarRentalSystem.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetAllUsers();

        public Task AddUser(User user);

    }
}
