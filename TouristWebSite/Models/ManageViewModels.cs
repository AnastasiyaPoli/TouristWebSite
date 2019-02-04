using DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
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
    }

    public class QuestionsViewModel
    {
        public List<Question> Questions { get; set; }

        [Display(Name = "Оберіть тему запитання: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Тема питання\".")]
        public string Theme { get; set; }

        [Display(Name = "Задайте питання: ")]
        [Required(ErrorMessage = "Необхідно заповнити поле \"Питання\".")]
        public string Text { get; set; }
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
}