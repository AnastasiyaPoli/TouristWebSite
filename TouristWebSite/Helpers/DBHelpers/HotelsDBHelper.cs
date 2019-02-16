using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DBHelpers
{
    public static class HotelsDBHelper
    {
        private static ApplicationDbContext context;

        public static List<Hotel> GetHotels(long destinationPointId)
        {
            using (context = new ApplicationDbContext())
            {
                return context.Hotels.Where(x => x.DestinationPointId == destinationPointId).ToList();
            }
        }

        public static Hotel GetHotelById(long id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.Hotels.FirstOrDefault(x => x.Id == id);
            }
        }
    }
}