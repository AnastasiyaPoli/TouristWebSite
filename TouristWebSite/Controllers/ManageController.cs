using DAL.DBHelpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TouristWebSite.Helpers;
using TouristWebSite.Models;

namespace TouristWebSite.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Пароль було успішно змінено."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Changes ? "Зміни було успішно збережено."
                : message == ManageMessageId.Error ? "Під час оновлення інформації сталась помилка."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : message == ManageMessageId.EmailConfirmation ? "На електронну пошту було надіслано посилання, за яким необхідно перейти для підтвердження."
                : message == ManageMessageId.EmailConfirmationSuccess ? "Електронну пошту було успішно підтверджено. Тепер її можна буде використати для відновлення паролю."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                IsSubscribed = UsersDBHelper.GetById(userId).IsSubscribed,
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId),
                EmailConfirmed = UsersDBHelper.GetById(userId).EmailConfirmed
            };
            return View(model);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ConfirmEmail()
        {
            var user = UsersDBHelper.GetById(User.Identity.GetUserId());
            var guid = Guid.NewGuid();
            var callbackUrl = Url.Action("ContinueConfirmation", "Manage", new { id = user.Id, token = guid }, protocol: Request.Url.Scheme);
            EmailSenderHelper.SendEmail(user.Email, "Підтвердження електронної пошти",
                "Для підтвердження електронної пошти, необхідно перейти за <a href=\"" + callbackUrl +
                "\">посиланням</a>.");
                                                                                       
            UsersDBHelper.AddEmailGuid(user.Id, guid);
            return RedirectToAction("Index", new { Message = ManageMessageId.EmailConfirmation });
        }

        [HttpGet]
        public ActionResult ContinueConfirmation(string id, string token)
        {
            var user = UsersDBHelper.GetById(id);

            if (user.EmailGuid.ToString() == token)
            {
                UsersDBHelper.ConfirmEmail(id);
                UsersDBHelper.AddEmailGuid(user.Id, null);
                return RedirectToAction("Index", new { Message = ManageMessageId.EmailConfirmationSuccess });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }

            // Custom errors

            //Old password
            var errorWrongPassword = result.Errors.FirstOrDefault(x => x == "Incorrect password.");
            if (errorWrongPassword != null)
            {
                string[] Errors = new string[1];
                Errors[0] = "Поточний пароль введено неправильно.";
                var rez = new IdentityResult(Errors);
                AddErrors(rez);
                return View(model);
            }

            // New password
            var errorWrongPassword1 = result.Errors.FirstOrDefault(x => x == "Passwords must have at least one non letter or digit character. Passwords must have at least one lowercase ('a'-'z'). Passwords must have at least one uppercase ('A'-'Z').");
            var errorWrongPassword2 = result.Errors.FirstOrDefault(x => x == "Passwords must have at least one non letter or digit character.");
            var errorWrongPassword3 = result.Errors.FirstOrDefault(x => x == "Passwords must have at least one non letter or digit character. Passwords must have at least one digit ('0'-'9'). Passwords must have at least one uppercase ('A'-'Z').");
            var errorWrongPassword4 = result.Errors.FirstOrDefault(x => x == "Passwords must have at least one non letter or digit character. Passwords must have at least one uppercase ('A'-'Z').");
            var errorWrongPassword5 = result.Errors.FirstOrDefault(x => x == "Passwords must have at least one non letter or digit character. Passwords must have at least one lowercase ('a'-'z').");
            var errorWrongPassword6 = result.Errors.FirstOrDefault(x => x == "Passwords must have at least one non letter or digit character. Passwords must have at least one digit ('0'-'9').");
            var errorWrongPassword7 = result.Errors.FirstOrDefault(x => x == "Passwords must have at least one non letter or digit character. Passwords must have at least one digit ('0'-'9'). Passwords must have at least one lowercase ('a'-'z').");
            var errorWrongPassword8 = result.Errors.FirstOrDefault(x => x == "Passwords must have at least one digit ('0'-'9'). Passwords must have at least one lowercase ('a'-'z'). Passwords must have at least one uppercase ('A'-'Z').");
            var errorWrongPassword9 = result.Errors.FirstOrDefault(x => x == "Passwords must have at least one lowercase ('a'-'z'). Passwords must have at least one uppercase ('A'-'Z').");
            var errorWrongPassword10 = result.Errors.FirstOrDefault(x => x == "Passwords must have at least one digit ('0'-'9'). Passwords must have at least one uppercase ('A'-'Z').");
            var errorWrongPassword11 = result.Errors.FirstOrDefault(x => x == "Passwords must have at least one digit ('0'-'9'). Passwords must have at least one lowercase ('a'-'z').");
            var errorWrongPassword12 = result.Errors.FirstOrDefault(x => x == "Passwords must have at least one uppercase ('A'-'Z').");
            var errorWrongPassword13 = result.Errors.FirstOrDefault(x => x == "Passwords must have at least one lowercase ('a'-'z').");
            var errorWrongPassword14 = result.Errors.FirstOrDefault(x => x == "Passwords must have at least one digit ('0'-'9').");
            if (errorWrongPassword1 != null || errorWrongPassword2 != null || errorWrongPassword3 != null || errorWrongPassword4 != null || errorWrongPassword5 != null || errorWrongPassword6 != null || errorWrongPassword7 != null || errorWrongPassword8 != null || errorWrongPassword9 != null || errorWrongPassword10 != null || errorWrongPassword11 != null || errorWrongPassword12 != null || errorWrongPassword13 != null || errorWrongPassword14 != null)
            {
                string[] Errors = new string[1];
                Errors[0] = "У паролі повинен бути хоча б один символ або цифра, хоча б одна велика літера a-z та одна мала літера A-Z.";
                var rez = new IdentityResult(Errors);
                AddErrors(rez);
                return View(model);
            }

            AddErrors(result);
            return View(model);
        }

        public ActionResult Subscription(bool subscribeOnly = false)
        {
            try
            {
                if (!(subscribeOnly && UsersDBHelper.GetById(User.Identity.GetUserId()).IsSubscribed))
                {
                    var userId = User.Identity.GetUserId();
                    UsersDBHelper.ChangeSubscription(userId);
                    return RedirectToAction("Index", new { Message = ManageMessageId.Changes });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Manage", action = "Index" });
            }
        }

        public ActionResult Statistics()
        {
            Dictionary<string, int> Data = new Dictionary<string, int>();
            var grouped = BookedToursDBHelper.GetBookedToursForUser(User.Identity.GetUserId())
                .OrderBy(x => x.Tour.DateStart)
                .GroupBy(ms => string.Format("{0}-{1}", ms.Tour.DateStart.Month, ms.Tour.DateStart.Year));

            foreach (var group in grouped)
            {
                var temp = group.Key;

                switch (temp.Substring(0, 2))
                {
                    case "1-":
                        {
                            temp = string.Concat("Січень ", temp.Substring(2, (temp.Length - 2)));
                        }
                        break;

                    case "2-":
                        {
                            temp = string.Concat("Лютий ", temp.Substring(2, (temp.Length - 2)));
                        }
                        break;

                    case "3-":
                        {
                            temp = string.Concat("Березень ", temp.Substring(2, (temp.Length - 2)));
                        }
                        break;

                    case "4-":
                        {
                            temp = string.Concat("Квітень ", temp.Substring(2, (temp.Length - 2)));
                        }
                        break;

                    case "5-":
                        {
                            temp = string.Concat("Травень ", temp.Substring(2, (temp.Length - 2)));
                        }
                        break;

                    case "6-":
                        {
                            temp = string.Concat("Червень ", temp.Substring(2, (temp.Length - 2)));
                        }
                        break;

                    case "7-":
                        {
                            temp = string.Concat("Липень ", temp.Substring(2, (temp.Length - 2)));
                        }
                        break;

                    case "8-":
                        {
                            temp = string.Concat("Серпень ", temp.Substring(2, (temp.Length - 2)));
                        }
                        break;

                    case "9-":
                        {
                            temp = string.Concat("Вересень ", temp.Substring(2, (temp.Length - 2)));
                        }
                        break;

                    case "10":
                        {
                            temp = string.Concat("Жовтень ", temp.Substring(3, (temp.Length - 3)));
                        }
                        break;

                    case "11":
                        {
                            temp = string.Concat("Листопад ", temp.Substring(3, (temp.Length - 3)));
                        }
                        break;

                    case "12":
                        {
                            temp = string.Concat("Грудень ", temp.Substring(3, (temp.Length - 3)));
                        }
                        break;
                }

                Data[temp] = group.Count();
            };

            Dictionary<string, int> DataConstructed = new Dictionary<string, int>();
            var groupedConstructed = ConstructedToursDBHelper.GetByUserId(User.Identity.GetUserId())
                .OrderBy(x => x.Route.Start)
                .GroupBy(ms => string.Format("{0}-{1}", ms.Route.Start.Month, ms.Route.Start.Year));

            foreach (var group in groupedConstructed)
            {
                var temp = group.Key;

                switch (temp.Substring(0, 2))
                {
                    case "1-":
                        {
                            temp = string.Concat("Січень ", temp.Substring(2, (temp.Length - 2)));
                        }
                        break;

                    case "2-":
                        {
                            temp = string.Concat("Лютий ", temp.Substring(2, (temp.Length - 2)));
                        }
                        break;

                    case "3-":
                        {
                            temp = string.Concat("Березень ", temp.Substring(2, (temp.Length - 2)));
                        }
                        break;

                    case "4-":
                        {
                            temp = string.Concat("Квітень ", temp.Substring(2, (temp.Length - 2)));
                        }
                        break;

                    case "5-":
                        {
                            temp = string.Concat("Травень ", temp.Substring(2, (temp.Length - 2)));
                        }
                        break;

                    case "6-":
                        {
                            temp = string.Concat("Червень ", temp.Substring(2, (temp.Length - 2)));
                        }
                        break;

                    case "7-":
                        {
                            temp = string.Concat("Липень ", temp.Substring(2, (temp.Length - 2)));
                        }
                        break;

                    case "8-":
                        {
                            temp = string.Concat("Серпень ", temp.Substring(2, (temp.Length - 2)));
                        }
                        break;

                    case "9-":
                        {
                            temp = string.Concat("Вересень ", temp.Substring(2, (temp.Length - 2)));
                        }
                        break;

                    case "10":
                        {
                            temp = string.Concat("Жовтень ", temp.Substring(3, (temp.Length - 3)));
                        }
                        break;

                    case "11":
                        {
                            temp = string.Concat("Листопад ", temp.Substring(3, (temp.Length - 3)));
                        }
                        break;

                    case "12":
                        {
                            temp = string.Concat("Грудень ", temp.Substring(3, (temp.Length - 3)));
                        }
                        break;
                }

                DataConstructed[temp] = group.Count();
            };


            var userId = User.Identity.GetUserId();

            StatisticsViewModel model = new StatisticsViewModel()
            {
                ToursCount = BookedToursDBHelper.GetBookedToursForUser(userId).Count(),
                IndividualToursCount = ConstructedToursDBHelper.GetByUserId(userId).Count(),
                QuestionsCount = QuestionsDBHelper.GetForUser(userId).Count(),
                Data = Data,
                DataConstrcuted = DataConstructed
            };

            return View(model);
        }

        public ActionResult BookedTours()
        {
            BookedToursViewModel model = new BookedToursViewModel()
            {
                BookedTours = BookedToursDBHelper.GetBookedToursForUser(User.Identity.GetUserId())
            };

            return View(model);
        }

        public ActionResult FavouriteTours(string message = "")
        {
            ViewBag.StatusMessage = message;

            FavouritesViewModel model = new FavouritesViewModel()
            {
                Favourites = FavouritesDBHelper.GetForUser(User.Identity.GetUserId())
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Questions(string message = "")
        {
            try
            {
                QuestionsViewModel model = new QuestionsViewModel()
                {
                    Questions = QuestionsDBHelper.GetForUser(User.Identity.GetUserId())
                };

                ViewBag.StatusMessage = message;

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Manage", action = "Index" });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AllQuestions(string message = "")
        {
            try
            {
                QuestionsViewModel model = new QuestionsViewModel()
                {
                    Questions = QuestionsDBHelper.GetAll()
                };

                ViewBag.StatusMessage = message;

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Manage", action = "Index" });
            }
        }

        [HttpPost]
        public ActionResult AddQuestions(QuestionsViewModel model)
        {
            try
            {
                QuestionsDBHelper.Add(model.Text, model.Theme, User.Identity.GetUserId());
                return RedirectToRoute(new { controller = "Manage", action = "Questions", message = "Запитання адміністраторам сайту було успішно додано." });
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Manage", action = "Index" });
            }
        }

        public ActionResult UserInfo(string itemId)
        {
            try
            {
                var userForEdit = UsersDBHelper.GetById(itemId);

                UserInfoViewModel model = new UserInfoViewModel()
                {
                    Id = itemId,
                    Name = userForEdit.Name,
                    Surname = userForEdit.Surname,
                    Gender = userForEdit.Gender,
                    DateOfBirth = userForEdit.DateOfBirth,
                    MaritalStatus = userForEdit.MaritalStatus,
                    Country = userForEdit.Country,
                    City = userForEdit.City,
                    Profession = userForEdit.Profession,
                    Biography = userForEdit.Biography
                };

                if (model.DateOfBirth == null)
                {
                    model.DateOfBirth = DateTime.Now;
                }

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Manage", action = "Index" });
            }
        }

        [HttpPost]
        public ActionResult UserInfo(UserInfoViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (model.DateOfBirth >= DateTime.Now)
                {
                    ModelState.AddModelError("DateOfBirth", "Необхідно вказати коректну дату народження.");
                    return View(model);
                }

                UsersDBHelper.UpdateUser(model);
                return RedirectToAction("Index", new { Message = ManageMessageId.Changes });
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Manage", action = "Index" });
            }
        }

        [HttpPost]
        public ActionResult Answer(long id, string answer)
        {
            try
            {
                QuestionsDBHelper.Answer(answer, id);
                var userId = QuestionsDBHelper.GetById(id).ApplicationUserId;
                EmailSenderHelper.SendEmail(UsersDBHelper.GetById(userId).Email, "Відповідь на запитання", "На Ваше запитання на сайті туристичної фірми \"Формула відпочинку \" надано відповідь. Ви можете переглянути її в особистому кабінеті.");
                return RedirectToRoute(new { controller = "Manage", action = "AllQuestions", message = "Відповідь на запитання було успішно надано." });
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Manage", action = "Index" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }

            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded
                ? RedirectToAction("ManageLogins")
                : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Users(string message = "")
        {
            ViewBag.StatusMessage = message;

            AllUsersViewModel model = new AllUsersViewModel()
            {
                Users = UsersDBHelper.GetAll()
            };

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult BlockingStatus(string userId)
        {
            try
            {
                UsersDBHelper.ChangeBlockStatus(userId);
                return RedirectToAction("Users", new { Message = "Зміни було успішно збережено." });
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Users", action = "Index" });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            EmailConfirmation,
            EmailConfirmationSuccess,
            Changes,
            Error
        }

        #endregion
    }
}