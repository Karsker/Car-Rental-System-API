using CarRentalSystem.Models;
using CarRentalSystem.Repositories;

namespace CarRentalSystem.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userrepo;

        public UserService(IUserRepository userrepo)
        {
            _userrepo = userrepo;
        }
        public Task<List<User>> GetAllUsers()
        {
            return _userrepo.GetAll();
        }
    }
}
