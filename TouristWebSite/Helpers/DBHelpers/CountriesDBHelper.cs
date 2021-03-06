﻿using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DBHelpers
{
    public static class CountriesDBHelper
    {
        private static ApplicationDbContext context;

        public static List<Country> GetCountries()
        {
            using (context = new ApplicationDbContext())
            {
                return context.Countries.ToList();
            }
        }

        public static Country GetCountryById(long id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.Countries.FirstOrDefault(x => x.Id == id);
            }
        }
    }
}