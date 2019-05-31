using DAL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.DBHelpers
{
    public static class TourExcusionsDBHelper
    {
        private static ApplicationDbContext context;

        public static List<TourExcursion> GetByTourId(long id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.TourExcursions.Where(x => x.ConstructedTourId == id).Include(y => y.Excursion).ToList();
            }
        }
    }
}