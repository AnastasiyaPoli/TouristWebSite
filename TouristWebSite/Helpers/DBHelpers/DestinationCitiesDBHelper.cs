using DAL.Models;
using System.Collections.Generic;
using System.Linq;

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
    }
}