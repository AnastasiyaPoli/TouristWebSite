using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TouristWebSite.Models
{
    public class UserInfoViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Ім'я: ")]
        public string Name { get; set; }

        [Display(Name = "Прізвище: ")]
        public string Surname { get; set; }

        [Display(Name = "Стать: ")]
        public string Gender { get; set; }

        [Display(Name = "Дата народження: ")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Сімейний стан: ")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Країна проживання: ")]
        public string Country { get; set; }

        [Display(Name = "Місто проживання: ")]
        public string City { get; set; }

        [Display(Name = "Професія: ")]
        public string Profession { get; set; }

        [Display(Name = "Коротка біографія: ")]
        public string Biography { get; set; }
    }

    public class AllUsersViewModel
    {
        public List<ApplicationUser> Users { get; set; }
    }
}