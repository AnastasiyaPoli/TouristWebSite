using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DBHelpers
{
    public static class DestinationCountriesDBHelper
    {
        private static ApplicationDbContext context;

        public static List<DestinationCountry> GetCountries()
        {
            using (context = new ApplicationDbContext())
            {
                return context.DestinationCountries.ToList();
            }
        }

        public static DestinationCountry GetCountryById(long id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.DestinationCountries.FirstOrDefault(x => x.Id == id);
            }
        }
    }
}