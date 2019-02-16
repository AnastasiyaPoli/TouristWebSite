using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DBHelpers
{
    public static class BackRoutesDBHelper
    {
        private static ApplicationDbContext context;

        public static List<BackRoute> GetBackRoutes(long leavePoint, long destinationPoint)
        {
            using (context = new ApplicationDbContext())
            {
                return context.BackRoutes.Where(x => x.LeavePointId == leavePoint && x.DestinationPointId == destinationPoint && x.Start > DateTime.Now).ToList();
            }
        }

        public static BackRoute GetBackRouteById(long id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.BackRoutes.FirstOrDefault(x => x.Id == id);
            }
        }
    }
}