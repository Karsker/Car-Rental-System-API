using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Repositories
{
    public class CarsRepository: ICarsRepository
    {
        private readonly AppDbContext _dbContext;

        public CarsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<List<Car>> GetAll()
        {
            return _dbContext.Cars.ToListAsync();
        }

        public ValueTask<Car?> GetById(int id)
        {
            return _dbContext.Cars.FindAsync(id);
        }
        public Task Add(Car car)
        {
            _dbContext.Cars.Add(car);
            return Task.FromResult(_dbContext.SaveChangesAsync());
        }

        public Task Update(Car car)
        {
            _dbContext.Cars.Entry(car).State = EntityState.Modified;
            return _dbContext.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            var carFromDb = _dbContext.Cars.Find(id);
            if (carFromDb is not null)
            {
                _dbContext.Remove(carFromDb);
            }

            return _dbContext.SaveChangesAsync();
        }
    }
}
