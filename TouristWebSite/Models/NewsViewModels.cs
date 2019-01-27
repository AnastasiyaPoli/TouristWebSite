using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TouristWebSite.Models
{
    public class ActiveNewsViewModel
    {
        public List<New> ActiveNews { get; set; }
        public List<Discount> ActiveDiscounts { get; set; }
    }

    public class NewViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Назва новини: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Назва новини\".")]
        public string Name { get; set; }

        [Display(Name = "Текст новини: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Текст новини\".")]
        public string Content { get; set; }

        [Display(Name = "Посилання на додаткову інформацію: ")]
        [RegularExpression("^http(s)?://([\\w-]+.)+[\\w-]+(/[\\w- ./?%&=])?$", ErrorMessage="Необхідно ввести коректне посилання.")]
        public string Link { get; set; }
    }

    public class DiscountViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Назва акції: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Назва акції\".")]
        public string Name { get; set; }

        [Display(Name = "Текст акції: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Текст акції\".")]
        public string Content { get; set; }

        [Display(Name = "Кінцева дата акції: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Кінцева дата акції\".")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Посилання на додаткову інформацію: ")]
        [RegularExpression("^http(s)?://([\\w-]+.)+[\\w-]+(/[\\w- ./?%&=])?$", ErrorMessage = "Необхідно ввести коректне посилання.")]
        public string Link { get; set; }
    }
}
