using System;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DBHelpers
{
    public static class DiscountsDBHelper
    {
        private static ApplicationDbContext context;

        public static List<Discount> GetActive()
        {
            using (context = new ApplicationDbContext())
            {
                return context.Discounts.Where(x => x.EndDate >= DateTime.Now).ToList();
            }
        }
    }
}