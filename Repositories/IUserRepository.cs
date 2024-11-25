using CarRentalSystem.Models;

namespace CarRentalSystem.Repositories
{ 
    public interface IUserRepository
    {
        public Task<List<User>> GetAll();

        public Task Add(User user);

        public Task<User?> GetByEmail(string email);
    }
}
