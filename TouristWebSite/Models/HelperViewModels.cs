using System;
using DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

    public class ConstructViewModel
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

        public IEnumerable<DestinationCountry> DestinationCountries { get; set; }

        [Display(Name = "Країна призначення: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Країна призначення\".")]
        public long DestinationCountry { get; set; }

        [Display(Name = "Місто призначення: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Місто призначення\".")]
        public long DestinationCity { get; set; }

        [Display(Name = "Точка призначення: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Точка призначення\".")]
        public long DestinationPoint { get; set; }

        [Display(Name = "Маршрут: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Маршрут\".")]
        public long Route { get; set; }

        [Display(Name = "Клас: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Клас\".")]
        public string Class { get; set; }

        [Display(Name = "Готель: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Готель\".")]
        public long Hotel { get; set; }

        [Display(Name = "Кількість людей: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Кількість людей\".")]
        [Range(1, 20, ErrorMessage = "Кількість людей повинна бути числом від 1 до 20.")]
        public long PeopleCount { get; set; }

        [Display(Name = "Клас номеру: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Клас номеру\".")]
        public string HotelClass { get; set; }

        [Display(Name = "Кількість екскурсій: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Кількість екскурсій\".")]
        [Range(0, 5, ErrorMessage = "Кількість екскурсій повинна бути числом від 0 до 5.")]
        public long ExcursionsCount { get; set; }

        [Display(Name = "Екскурсія №1: ")]
        public long? Excursion1 { get; set; }

        [Display(Name = "Екскурсія №2: ")]
        public long? Excursion2 { get; set; }

        [Display(Name = "Екскурсія №3: ")]
        public long? Excursion3 { get; set; }

        [Display(Name = "Екскурсія №4: ")]
        public long? Excursion4 { get; set; }

        [Display(Name = "Екскурсія №5: ")]
        public long? Excursion5 { get; set; }

        [Display(Name = "Зворотний маршрут: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Зворотний маршрут\".")]
        public long BackRoute { get; set; }

        [Display(Name = "Клас: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Клас\".")]
        public string BackClass { get; set; }

        public long Price { get; set; }
    }

    public class ConstructRateViewModel
    {
        public long Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public long PeopleCount { get; set; }
        public long Price { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Transport { get; set; }
        public string LeavePoint { get; set; }
        public string DestinationCountry { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationPoint { get; set; }
        public string Route { get; set; }
        public string Class { get; set; }
        public string Hotel { get; set; }
        public string HotelClass { get; set; }
        public string Excursions { get; set; }
        public string BackRoute { get; set; }
        public string BackClass { get; set; }
        public string Comment { get; set; }
        public string Mark { get; set; }
    }

    public class ListConstructRateViewModels
    {
        public List<ConstructRateViewModel> ConstructRateViewModels { get; set; }
    }
}