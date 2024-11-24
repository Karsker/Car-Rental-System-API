using CarRentalSystem.Models;

namespace CarRentalSystem.Repositories
{ 
    public interface IUserRepository
    {
        public Task<List<User>> GetAll();
    }
}
