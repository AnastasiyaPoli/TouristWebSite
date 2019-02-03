using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TouristWebSite.Models
{
    public class ActiveToursViewModel
    {
        public List<Tour> ActiveTours { get; set; }
    }

    public class ChosenTourViewModel
    {
        public Tour ChosenTour { get; set; }
    }

    public class TourViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Назва туру: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Назва\".")]
        public string Name { get; set; }

        [Display(Name = "Місце відпочинку: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Місце відпочинку\".")]
        public string Place { get; set; }

        [Display(Name = "Опис туру: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Опис туру\".")]
        public string Description { get; set; }

        [Display(Name = "Дата початку туру: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Дата початку туру\".")]
        public DateTime DateStart { get; set; }

        [Display(Name = "Дата закінчення туру: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Дата кінця туру\".")]
        public DateTime DateEnd { get; set; }

        [Display(Name = "Ціна туру на людину: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Ціна туру на людину\".")]
        [Range(1, 1000000, ErrorMessage = "Ціна туру повинна бути числом від 1 до 1000000.")]
        public long Price { get; set; }

        [Display(Name = "Посилання на маршрут туру: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Посилання на маршрут туру\".")]
        [RegularExpression("^(http|https)://www.google.com/maps/embed.*", ErrorMessage = "Необхідно ввести коректне посилання.")]
        public string RouteLink { get; set; }
    }

    public partial class ImageViewModel
    {
        public Tour Tour { get; set; }
        public string ImagePath { get; set; }
    }
}
