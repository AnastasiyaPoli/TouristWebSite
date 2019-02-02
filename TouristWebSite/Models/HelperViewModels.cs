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
}