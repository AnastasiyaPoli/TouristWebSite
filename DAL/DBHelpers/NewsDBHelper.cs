using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DBHelpers
{
    public static class NewsDBHelper
    {
        private static ApplicationDbContext context;

        public static List<New> GetActive()
        {
            using (context = new ApplicationDbContext())
            {
                return context.News.Where(x => x.IsActive).ToList();
            }
        }
    }
}