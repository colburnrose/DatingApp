using System;
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
        public async Task<User> Login(string username, string password)
        {
            // get user by username
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if(user == null) return null;
            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            return null;
            
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {   
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                // loop through computedHash and compare with passwordHash
                for(int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i]) 
                    return false;
                }
                return true;
            }

            // loop 
        }

        public async Task<User> RegisterUser(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt); 

            // get user passwordHash and passwordSalt
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            // save the user to the db.
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
        // out: passing a reference to the variables. 
        // so when it's updated in the method it will also do the same for the variables inside RegisterUser().
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // using statement disposes when we're finished with it.
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                // gets the password as a byte[]. & gives the ComputeHash as an array of bytes.
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }

        public async Task<bool> UserExists(string username)
        {
            // As AnyAsync is already a bool - we can simply return it.
            return await _context.Users.AnyAsync(x => x.UserName == username);
        }
    }
}