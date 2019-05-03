using DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace DAL.DBHelpers
{
    public static class CitiesDBHelper
    {
        private static ApplicationDbContext context;

        public static List<City> GetCitiesForCountry(long countryId)
        {
            using (context = new ApplicationDbContext())
            {
                return context.Cities.Where(x => x.CountryId == countryId).ToList();
            }
        }

        public static City GetCityById(long id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.Cities.Include(x => x.Country).FirstOrDefault(x => x.Id == id);
            }
        }
    }
}