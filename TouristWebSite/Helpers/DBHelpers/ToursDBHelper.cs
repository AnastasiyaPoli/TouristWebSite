using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DBHelpers
{
    public static class ToursDBHelper
    {
        private static ApplicationDbContext context;

        public static List<Tour> GetActive()
        {
            using (context = new ApplicationDbContext())
            {
                return context.Tours.Where(x => x.IsActive).ToList();
            }
        }
    }
}