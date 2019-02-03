using DAL.Models;
using System.Linq;
using TouristWebSite.Models;

namespace DAL.DBHelpers
{
    public static class BookedToursDBHelper
    {
        private static ApplicationDbContext context;

        public static long BookTour(TourBookingViewModel model, string UserId)
        {
            using (context = new ApplicationDbContext())
            {
                BookedTour tour = new BookedTour()
                {
                    TourId = model.TourId,
                    ApplicationUserId = UserId,
                    PeopleCount = model.PeopleCount,
                    Comment = model.Comment
                };

                var newTour = context.BookedTours.Add(tour);
                context.SaveChanges();
                return newTour.Id;
            }
        }

        public static bool Check(string userId, long tourId)
        {
            using (context = new ApplicationDbContext())
            {
                return (context.BookedTours.FirstOrDefault(x => x.ApplicationUserId == userId && x.TourId == tourId) != null);
            }
        }
    }
}