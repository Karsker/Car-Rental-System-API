using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext context)
        {
            _dbContext = context;
        }
        public Task<List<User>> GetAll()
        {
            return _dbContext.Users.ToListAsync();
        }
    }
}
