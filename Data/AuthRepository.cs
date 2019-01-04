using System;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        
        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string name, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == name);
            
            if(user == null)
                return null;

            if(!VerifyPasswordHash(password, user.Password, user.Salt))
                return null;

            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] hash, salt;
            CreateHash(password, out hash, out salt);

            user.Password = hash;
            user.Salt = salt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExists(string name)
        {
            return await _context.Users.AnyAsync(x => x.UserName == name);
        }

        private void CreateHash(string password, out byte[] hash, out byte[] salt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                salt = hmac.Key;
            }
        }

        private bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return passwordHash.SequenceEqual(hash);
            }
        }
    }
}