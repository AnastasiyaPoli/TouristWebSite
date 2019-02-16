using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DBHelpers
{
    public static class LeavePointDBHelper
    {
        private static ApplicationDbContext context;

        public static List<LeavePoint> GetLeavePointsByCityAndTransport(long cityId, long transportId)
        {
            using (context = new ApplicationDbContext())
            {
                return context.LeavePoints.Where(x => x.CityId == cityId && x.TransportId == transportId).ToList();
            }
        }

        public static LeavePoint GetLeavePointById(long id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.LeavePoints.FirstOrDefault(x => x.Id == id);
            }
        }
    }
}