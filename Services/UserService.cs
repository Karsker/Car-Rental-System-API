using System.Text;
using CarRentalSystem.Models;
using CarRentalSystem.Repositories;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace CarRentalSystem.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userrepo;
        
        public UserService(IUserRepository userrepo)
        {
            _userrepo = userrepo;
        }

       
        // Function to compute hash
        public static string ComputeHash(string source)
        {
            byte[] tmpSource;
            byte[] tmpHash;

            tmpSource = Encoding.ASCII.GetBytes(source);
            tmpHash = SHA256.HashData(tmpSource);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < tmpHash.Length; i++)
            {
                sb.Append(tmpHash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public Task<List<User>> GetAllUsers()
        {
            return _userrepo.GetAll();
        }

        public Task AddUser(User user)
        {

            user.Password = ComputeHash(user.Password);
            return _userrepo.Add(user);
        }

        public Task<User?> GetUserByEmail(string email)
        {
            return _userrepo.GetByEmail(email);
        }

        public ValueTask<User?> GetUserById(int id)
        {
            return _userrepo.GetById(id);
        }


    }
}
