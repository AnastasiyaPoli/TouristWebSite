using System.Collections.Generic;
using System.Linq;
using TouristWebSite.Models;

namespace DAL.DBHelpers
{
    public static class UsersDBHelper
    {
        private static ApplicationDbContext context;

        public static List<ApplicationUser> GetAllActive()
        {
            using (context = new ApplicationDbContext())
            {
                return context.Users.Where(x => x.IsActive).ToList();
            }
        }

        public static List<ApplicationUser> GetAll()
        {
            using (context = new ApplicationDbContext())
            {
                return context.Users.ToList();
            }
        }

        public static ApplicationUser GetById(string id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.Users.FirstOrDefault(x => x.Id == id);
            }
        }

        public static void ChangeSubscription(string id)
        {
            using (context = new ApplicationDbContext())
            {
                context.Users.FirstOrDefault(x => x.Id == id).IsSubscribed = !context.Users.FirstOrDefault(x => x.Id == id).IsSubscribed;
                context.SaveChanges();
            }
        }

        public static void ChangeBlockStatus(string id)
        {
            using (context = new ApplicationDbContext())
            {
                context.Users.FirstOrDefault(x => x.Id == id).IsActive = !context.Users.FirstOrDefault(x => x.Id == id).IsActive;
                context.SaveChanges();
            }
        }

        public static void UpdateUser(UserInfoViewModel user)
        {
            using (context = new ApplicationDbContext())
            {
                var userForUpdate = context.Users.FirstOrDefault(x => x.Id == user.Id);

                userForUpdate.Name = user.Name;
                userForUpdate.Surname = user.Surname;
                userForUpdate.Gender = user.Gender;
                userForUpdate.DateOfBirth = user.DateOfBirth;
                userForUpdate.MaritalStatus = user.MaritalStatus;
                userForUpdate.Country = user.Country;
                userForUpdate.City = user.City;
                userForUpdate.Profession = user.Profession;
                userForUpdate.Biography = user.Biography;

                context.SaveChanges();
            }
        }

        public static void UpdateUserAdditional(AdditionalInfoViewModel user)
        {
            using (context = new ApplicationDbContext())
            {
                var userForUpdate = context.Users.FirstOrDefault(x => x.Id == user.Id);

                userForUpdate.TypeOfStudies = user.TypeOfStudies;
                userForUpdate.StudiesDescription = user.StudiesDescription;
                userForUpdate.TypeOfWork = user.TypeOfWork;
                userForUpdate.WorkDescription = user.WorkDescription;
                userForUpdate.SportsDescription = user.SportsDescription;
                userForUpdate.MusicDescription = user.MusicDescription;
                userForUpdate.FilmsDescription = user.FilmsDescription;
                userForUpdate.BooksDescription = user.BooksDescription;
                userForUpdate.HobbiesDescription = user.HobbiesDescription;
                userForUpdate.PetsDescription = user.PetsDescription;
                userForUpdate.OtherInfo = user.OtherInfo;

                context.SaveChanges();
            }
        }
    }
}