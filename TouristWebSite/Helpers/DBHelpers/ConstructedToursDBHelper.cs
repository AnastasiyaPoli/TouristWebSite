using DAL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TouristWebSite.Models;

namespace DAL.DBHelpers
{
    public static class ConstructedToursDBHelper
    {
        private static ApplicationDbContext context;

        public static List<ConstructedTour> GetAll()
        {
            using (context = new ApplicationDbContext())
            {
                return context.ConstructedTours.ToList();
            }
        }

        public static List<ConstructedTour> GetByUserId(string id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.ConstructedTours
                    .Where(x => x.ApplicationUserId == id)
                    .Include(x => x.Route)
                    .Include(x => x.BackRoute)
                    .Include(x => x.Hotel)
                    .ToList();
            }
        }

        public static ConstructedTour GetById(long id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.ConstructedTours
                    .Include(x => x.Route)
                    .Include(x => x.BackRoute)
                    .Include(x => x.Hotel)
                    .FirstOrDefault(x => x.Id == id);
            }
        }

        public static long Add(ConstructViewModel model, string userId)
        {
            List<Excursion> excursions = new List<Excursion>();

            if (model.ExcursionsCount > 0)
            {
                excursions.Add(ExcursionsDBHelper.GetExcursionById(model.Excursion1));
            }

            if (model.ExcursionsCount > 1)
            {
                excursions.Add(ExcursionsDBHelper.GetExcursionById(model.Excursion2));
            }

            if (model.ExcursionsCount > 2)
            {
                excursions.Add(ExcursionsDBHelper.GetExcursionById(model.Excursion3));
            }

            if (model.ExcursionsCount > 3)
            {
                excursions.Add(ExcursionsDBHelper.GetExcursionById(model.Excursion4));
            }

            if (model.ExcursionsCount > 4)
            {
                excursions.Add(ExcursionsDBHelper.GetExcursionById(model.Excursion5));
            }

            using (context = new ApplicationDbContext())
            {
                var newTour = new ConstructedTour()
                {
                    RouteId = model.Route,
                    HotelId = model.Hotel,
                    PeopleCount = model.PeopleCount,
                    Class = model.Class,
                    HotelClass = model.HotelClass,
                    BackRouteId = model.BackRoute,
                    BackClass = model.BackClass,
                    ApplicationUserId = userId,
                    Price = model.Price,
                    AdditionalInfo = model.AdditionalInfo
                };

                context.ConstructedTours.Add(newTour);
                context.SaveChanges();

                foreach (var excursion in excursions)
                {
                    context.TourExcursions.Add(new TourExcursion()
                    {
                        ConstructedTourId = newTour.Id,
                        ExcursionId = excursion.Id
                    });
                }
                context.SaveChanges();

                return newTour.Id;
            }
        }

        public static void AddComment(string comment, string mark, long id)
        {
            int numberMark = 0;

            switch (mark)
            {
                case "Ідеально":
                {
                    numberMark = 5;
                }
                    break;

                case "Добре":
                {
                    numberMark = 4;
                }
                    break;

                case "Задовільно":
                {
                    numberMark = 3;
                }
                    break;

                case "Погано":
                {
                    numberMark = 2;
                }
                    break;

                case "Жахливо":
                {
                    numberMark = 1;
                }
                    break;

                default:
                {
                    numberMark = 0;
                }
                    break;
            }

            using (context = new ApplicationDbContext())
            {
                var tour = context.ConstructedTours.FirstOrDefault(x => x.Id == id);
                tour.NumberMark = numberMark;
                tour.Comment = comment;
                context.SaveChanges();
            }
        }
    }
}