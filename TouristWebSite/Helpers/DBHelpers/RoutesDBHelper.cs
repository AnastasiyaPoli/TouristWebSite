using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DBHelpers
{
    public static class RoutesDBHelper
    {
        private static ApplicationDbContext context;

        public static List<Route> GetRoutesForLeavePoint(long leavePoint)
        {
            using (context = new ApplicationDbContext())
            {
                return context.Routes.Where(x => x.LeavePointId == leavePoint && x.Start > DateTime.Now).ToList();
            }
        }
    }
}