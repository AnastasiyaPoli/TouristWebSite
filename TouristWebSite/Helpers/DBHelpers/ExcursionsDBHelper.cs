using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DBHelpers
{
    public static class ExcursionsDBHelper
    {
        private static ApplicationDbContext context;

        public static List<Excursion> GetExcursions(long hotelId)
        {
            using (context = new ApplicationDbContext())
            {
                return context.Excursions.Where(x => x.HotelId == hotelId).ToList();
            }
        }

        public static Excursion GetExcursionById(long? id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.Excursions.FirstOrDefault(x => x.Id == id);
            }
        }
    }
}