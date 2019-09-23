using System.Collections.Generic;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        public static void SeedUsers(DataContext context) 
        {
            if(!context.Users.Any()) 
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                foreach(var user in users) 
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("password", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.UserName = user.UserName.ToLower();
                    context.Users.Add(user);
                }
                context.SaveChanges();
            }
        }

         private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // using statement disposes when we're finished with it.
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                // gets the password as a byte[]. & gives the ComputeHash as an array of bytes.
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }
    }
}