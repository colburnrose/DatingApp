using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTOs
{
    public class UserForLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}