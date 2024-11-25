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

        public Task Add(User user)
        {    
            _dbContext.Users.Add(user);
            return _dbContext.SaveChangesAsync();
        }

        public Task<User?> GetByEmail(string email)
        {
            return _dbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        } 
    }
}
