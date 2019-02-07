using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TouristWebSite.Models
{
    public class ActiveToursViewModel
    {
        public List<Tour> ActiveTours { get; set; }
        public List<Favourite> Favourites { get; set; }
    }

    public class ChosenTourViewModel
    {
        public Tour ChosenTour { get; set; }
        public long ChosenTourId { get; set; }
        public List<CommentViewModel> Comments { get; set; }

        [Display(Name = "Оберіть оцінку: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Оцінка\".")]
        public string Mark { get; set; }


        [Display(Name = "Додайте коментар: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Коментар\".")]
        public string Text { get; set; }
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
        [MinLength(100, ErrorMessage = "Довжина опису туру повинна бути не менше 100 символів.")]
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
        public long TourId { get; set; }

        [Display(Name = "Оберіть фото: ")]
        public string ImagePath { get; set; }
    }

    public class CommentViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Додайте коментар: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Коментар\".")]
        public string Text { get; set; }

        [Display(Name = "Оберіть оцінку: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Оцінка\".")]
        public string Mark { get; set; }

        public bool WasBooked { get; set; }

        public ApplicationUser User { get; set; }
    }
}
