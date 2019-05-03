using DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace DAL.DBHelpers
{
    public static class DestinationCitiesDBHelper
    {
        private static ApplicationDbContext context;

        public static List<DestinationCity> GetCitiesForCountry(long countryId)
        {
            using (context = new ApplicationDbContext())
            {
                return context.DestinationCities.Where(x => x.DestinationCountryId == countryId).ToList();
            }
        }

        public static DestinationCity GetCityById(long id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.DestinationCities.Include(x => x.DestinationCountry).FirstOrDefault(x => x.Id == id);
            }
        }
    }
}