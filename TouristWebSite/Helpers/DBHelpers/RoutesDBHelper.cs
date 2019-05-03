using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.DBHelpers
{
    public static class RoutesDBHelper
    {
        private static ApplicationDbContext context;

        public static List<Route> GetRoutes(long leavePoint, long destinationPoint)
        {
            using (context = new ApplicationDbContext())
            {
                return context.Routes.Where(x => x.LeavePointId == leavePoint && x.DestinationPointId == destinationPoint && x.Start > DateTime.Now).ToList();
            }
        }

        public static Route GetRouteById(long id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.Routes.Include(x => x.LeavePoint).Include(x => x.DestinationPoint).FirstOrDefault(x => x.Id == id);
            }
        }
    }
}