using System;
using System.ComponentModel.DataAnnotations;
using DAL.Models;

namespace TouristWebSite.Models
{
    public class TourBookingViewModel
    {
        public string Description { get; set; }

        public long TourId { get; set; }

        public long Price { get; set; }

        [Display(Name = "Кількість людей: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Кількість людей\".")]
        [Range(1, 20, ErrorMessage = "Кількість людей повинна бути числом від 1 до 20.")]
        public int PeopleCount { get; set; }

        [Display(Name = "Зауваження / додаткова інформація: ")]
        public string Comment { get; set; }
    }
}