using DAL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.DBHelpers
{
    public static class RecommendationsDBHelper
    {
        private static ApplicationDbContext context;

        public static List<Recommendation> GetRecommendationsForUser(string id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.Recommendations.Where(x => x.ApplicationUserId == id).Include(y => y.ConstructedTour) .ToList();
            }
        }

        public static void SaveRecommendations(List<ConstructedTour> tours, string userId)
        {
            using (context = new ApplicationDbContext())
            {
                var forRemoving = context.Recommendations.Where(x => x.ApplicationUserId == userId);
                context.Recommendations.RemoveRange(forRemoving);

                foreach (var tour in tours)
                {
                    context.Recommendations.Add(
                        new Recommendation()
                        {
                            ConstructedTourId = tour.Id,
                            ApplicationUserId = userId
                        });
                }

                context.SaveChanges();
            }
        }
    }
}