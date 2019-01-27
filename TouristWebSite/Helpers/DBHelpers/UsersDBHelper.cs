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
    }
}