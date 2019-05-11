using DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TouristWebSite.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public bool IsSubscribed { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
        public bool EmailConfirmed { get; set; }
    }

    public class QuestionsViewModel
    {
        public List<Question> Questions { get; set; }

        [Display(Name = "Оберіть тему: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Тема запитання\".")]
        public string Theme { get; set; }

        [Display(Name = "Введіть запитання: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Текст запитання\".")]
        public string Text { get; set; }
    }

    public class QuestionViewModel
    {
        public Question Question { get; set; }

        public long QuestionId { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Відповідь на питання: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Відповідь на запитання\".")]
        public string Text { get; set; }
    }

    public class FavouritesViewModel
    {
        public List<Favourite> Favourites { get; set; }
    }

    public class StatisticsViewModel
    {
        public int ToursCount { get; set; }
        public int IndividualToursCount { get; set; }
        public int QuestionsCount { get; set; }
        public Dictionary<string, int> Data { get; set; }
        public Dictionary<string, int> DataConstrcuted { get; set; }
    }

    public class BookedToursViewModel
    {
        public List<BookedTour> BookedTours { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required(ErrorMessage = "Необхідно заповнити поле \"Новий пароль\".")]
        [StringLength(100, ErrorMessage = "Мінімальна довжина пароля: {2} символів.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новий пароль:")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердіть новий пароль:")]
        [Compare("NewPassword", ErrorMessage = "Пароль та підтвердження пароля не співпадають.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Необхідно заповнити поле \"Поточний пароль\".")]
        [DataType(DataType.Password)]
        [Display(Name = "Поточний пароль:")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Необхідно заповнити поле \"Новий пароль\".")]
        [StringLength(100, ErrorMessage = "Мінімальна довжина пароля: {2} символів.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новий пароль:")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердіть новий пароль:")]
        [Compare("NewPassword", ErrorMessage = "Пароль та підтвердження пароля не співпадають.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }

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
        public DateTime? DateOfBirth { get; set; }

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