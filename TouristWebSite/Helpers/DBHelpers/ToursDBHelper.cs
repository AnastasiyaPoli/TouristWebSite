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

        public static Tour GetTour(long id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.Tours.FirstOrDefault(x => x.IsActive && x.Id == id);
            }
        }

        public static void Deactivate(long id)
        {
            using (context = new ApplicationDbContext())
            {
                context.Tours.FirstOrDefault(x => x.Id == id).IsActive = false;
                context.SaveChanges();
            }
        }
    }
}