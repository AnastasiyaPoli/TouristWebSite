using DAL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.DBHelpers
{
    public static class CommentsDBHelper
    {
        private static ApplicationDbContext context;

        public static List<Comment> GetActiveForTour(long tourId)
        {
            using (context = new ApplicationDbContext())
            {
                return context.Comments.Where(x => x.IsActive && x.TourId == tourId).Include(y => y.User).ToList();
            }
        }

        public static void Deactivate(long id)
        {
            using (context = new ApplicationDbContext())
            {
                context.Comments.FirstOrDefault(x => x.Id == id).IsActive = false;
                context.SaveChanges();
            }
        }

        public static void Add(string text, string userId, long tourId, string mark)
        {
            using (context = new ApplicationDbContext())
            {
                var newComment = new Comment()
                {
                    ApplicationUserId = userId,
                    TourId = tourId,
                    Text = text,
                    Mark = mark,
                    IsActive = true
                };

                context.Comments.Add(newComment);
                context.SaveChanges();
            }
        }
    }
}