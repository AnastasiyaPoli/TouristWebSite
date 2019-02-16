using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DBHelpers
{
    public static class DestinationPointDBHelper
    {
        private static ApplicationDbContext context;

        public static List<DestinationPoint> GetDestinationPointsByCityAndTransport(long cityId, long transportId)
        {
            using (context = new ApplicationDbContext())
            {
                return context.DestinationPoints.Where(x => x.DestinationCityId == cityId && x.TransportId == transportId).ToList();
            }
        }

        public static DestinationPoint GetDestinationPointById(long id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.DestinationPoints.FirstOrDefault(x => x.Id == id);
            }
        }
    }
}