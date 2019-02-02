using System;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;
using TouristWebSite.Models;

namespace DAL.DBHelpers
{
    public static class UsersDBHelper
    {
        private static ApplicationDbContext context;

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
    }
}