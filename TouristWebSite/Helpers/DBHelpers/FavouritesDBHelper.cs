using DAL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.DBHelpers
{
    public static class FavouritesDBHelper
    {
        private static ApplicationDbContext context;

        public static List<Favourite> GetForUser(string userId)
        {
            using (context = new ApplicationDbContext())
            {
                return context.Favourites.Where(x => x.ApplicationUserId == userId).Include(x => x.User).Include(y => y.Tour).ToList();
            }
        }

        public static void Add(string userId, long tourId)
        {
            using (context = new ApplicationDbContext())
            {
                var newFavourite = new Favourite()
                {
                    ApplicationUserId = userId,
                    TourId = tourId
                };

                context.Favourites.Add(newFavourite);
                context.SaveChanges();
            }
        }

        public static void Delete(long favouriteId)
        {
            using (context = new ApplicationDbContext())
            {
                var favouriteToRemove = context.Favourites.FirstOrDefault(x => x.Id == favouriteId);
                context.Favourites.Remove(favouriteToRemove);
                context.SaveChanges();
            }
        }
    }
}