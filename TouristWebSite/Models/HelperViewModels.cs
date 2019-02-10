using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Models;

namespace TouristWebSite.Models
{
    public class AdditionalInfoViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Тип освіти: ")]
        public string TypeOfStudies { get; set; }

        [Display(Name = "Короткий опис освіти: ")]
        public string StudiesDescription { get; set; }

        [Display(Name = "Тип роботи: ")]
        public string TypeOfWork { get; set; }

        [Display(Name = "Короткий опис роботи: ")]
        public string WorkDescription { get; set; }

        [Display(Name = "Улюблені види спорту: ")]
        public string SportsDescription { get; set; }

        [Display(Name = "Улюблена музика: ")]
        public string MusicDescription { get; set; }

        [Display(Name = "Улюблені фільми: ")]
        public string FilmsDescription { get; set; }

        [Display(Name = "Улюблені книги: ")]
        public string BooksDescription { get; set; }

        [Display(Name = "Інші хобі: ")]
        public string HobbiesDescription { get; set; }

        [Display(Name = "Домашні улюбленці: ")]
        public string PetsDescription { get; set; }

        [Display(Name = "Коментарі: ")]
        public string OtherInfo { get; set; }
    }

    public class CountriesViewModel
    {
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<Transport> Transports { get; set; }

        [Display(Name = "Країна відправлення: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Країна відправлення\".")]
        public long Country { get; set; }

        [Display(Name = "Місто відправлення: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Місто відправлення\".")]
        public long City { get; set; }

        [Display(Name = "Тип транспорту: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Тип транспорту\".")]
        public long Transport { get; set; }

        [Display(Name = "Точка відправлення: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Точка відправлення\".")]
        public long LeavePoint { get; set; }

        [Display(Name = "Маршрут: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Маршрут\".")]
        public long Route { get; set; }
    }
}