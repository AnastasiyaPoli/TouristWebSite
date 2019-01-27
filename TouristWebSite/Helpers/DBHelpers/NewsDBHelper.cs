using System;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;
using TouristWebSite.Models;

namespace DAL.DBHelpers
{
    public static class NewsDBHelper
    {
        private static ApplicationDbContext context;

        public static List<New> GetActive()
        {
            using (context = new ApplicationDbContext())
            {
                return context.News.Where(x => x.IsActive).ToList();
            }
        }

        public static New GetById(int id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.News.FirstOrDefault(x => x.Id == id && x.IsActive);
            }
        }

        public static void Deactivate(int id)
        {
            using (context = new ApplicationDbContext())
            {
                context.News.FirstOrDefault(x => x.Id == id).IsActive = false;
                context.SaveChanges();
            }
        }

        public static void Add(NewViewModel model)
        {
            using (context = new ApplicationDbContext())
            {
                var newNew = new New()
                {
                    Name = model.Name,
                    Content = model.Content,
                    Link = model.Link,
                    Date = DateTime.Now,
                    IsActive = true
                };

                context.News.Add(newNew);
                context.SaveChanges();
            }
        }

        public static void Edit(NewViewModel model)
        {
            using (context = new ApplicationDbContext())
            {
                var newForEdit = context.News.FirstOrDefault(x => x.Id == model.Id);

                newForEdit.Name = model.Name;
                newForEdit.Content = model.Content;
                newForEdit.Link = model.Link;

                context.SaveChanges();
            }
        }
    }
}