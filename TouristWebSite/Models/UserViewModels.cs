using System;

namespace TouristWebSite.Models
{
    public class UserInfoViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Profession { get; set; }
        public string Biography { get; set; }
    }
}