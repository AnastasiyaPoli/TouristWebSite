using System;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;
using TouristWebSite.Models;

namespace DAL.DBHelpers
{
    public static class DiscountsDBHelper
    {
        private static ApplicationDbContext context;

        public static List<Discount> GetActive()
        {
            using (context = new ApplicationDbContext())
            {
                return context.Discounts.Where(x => x.EndDate >= DateTime.Now).ToList();
            }
        }

        public static Discount GetById(int id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.Discounts.FirstOrDefault(x => x.Id == id && x.EndDate >= DateTime.Now);
            }
        }

        public static void Add(DiscountViewModel model)
        {
            using (context = new ApplicationDbContext())
            {
                var newDiscount = new Discount()
                {
                    Name = model.Name,
                    Content = model.Content,
                    Link = model.Link,
                    StartDate = DateTime.Now,
                    EndDate = model.EndDate
                };

                context.Discounts.Add(newDiscount);
                context.SaveChanges();
            }
        }

        public static void Edit(DiscountViewModel model)
        {
            using (context = new ApplicationDbContext())
            {
                var discountForEdit = context.Discounts.FirstOrDefault(x => x.Id == model.Id);

                discountForEdit.Name = model.Name;
                discountForEdit.Content = model.Content;
                discountForEdit.EndDate = model.EndDate;
                discountForEdit.Link = model.Link;

                context.SaveChanges();
            }
        }
    }
}