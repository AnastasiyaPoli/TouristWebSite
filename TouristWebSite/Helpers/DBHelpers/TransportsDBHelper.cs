using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DBHelpers
{
    public static class TransportsDBHelper
    {
        private static ApplicationDbContext context;

        public static List<Transport> GetTransports()
        {
            using (context = new ApplicationDbContext())
            {
                return context.Transports.ToList();
            }
        }
    }
}