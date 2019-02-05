using DAL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.DBHelpers
{
    public static class QuestionsDBHelper
    {
        private static ApplicationDbContext context;

        public static List<Question> GetForUser(string userId)
        {
            using (context = new ApplicationDbContext())
            {
                return context.Questions.Where(x => x.User.Id == userId).Include(y => y.User).ToList();
            }
        }

        public static List<Question> GetAll()
        {
            using (context = new ApplicationDbContext())
            {
                return context.Questions.Include(y => y.User).ToList();
            }
        }

        public static Question GetById(long questionId)
        {
            using (context = new ApplicationDbContext())
            {
                return context.Questions.Include(y => y.User).FirstOrDefault(x => x.Id == questionId);
            }
        }

        public static void Add(string text, string theme, string userId)
        {
            using (context = new ApplicationDbContext())
            {
                var newQuestion = new Question()
                {
                    ApplicationUserId = userId,
                    Text = text,
                    Theme = theme,
                    Answer = string.Empty
                };

                context.Questions.Add(newQuestion);
                context.SaveChanges();
            }
        }

        public static void Answer(string text, long questionId)
        {
            using (context = new ApplicationDbContext())
            {
                context.Questions.FirstOrDefault(x => x.Id == questionId).Answer = text;
                context.SaveChanges();
            }
        }
    }
}