using System;
using System.Collections.Generic;

namespace DatingApp.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Gender {get;set;}
        public DateTime DateOfBirth {get;set;}
        public string NickName {get;set;}
        public DateTime DateCreated {get;set;}
        public DateTime LastActive {get;set;}
        public string Bio { get; set; }
        public string LookingFor { get; set; }
        public string  City { get; set; }
        public string Country {get;set;}
        public ICollection<Photo> Photos {get;set;}
    }
}