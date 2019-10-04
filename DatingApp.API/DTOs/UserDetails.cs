using System;
using System.Collections.Generic;
using DatingApp.API.Models;

namespace DatingApp.API.DTOs
{
    public class UserDetails
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Gender {get;set;}
        public int Age {get;set;}
        public string NickName {get;set;}
        public DateTime DateCreated {get;set;}
        public DateTime LastActive {get;set;}
        public string Bio { get; set; }
        public string LookingFor { get; set; }
        public string  City { get; set; }
        public string Country {get;set;} 
        public string PhotoUrl { get; set; }
        public ICollection<PhotoDetails> Photos { get; set; }
    }
}