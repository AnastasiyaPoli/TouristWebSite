using System;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;
using TouristWebSite.Models;

namespace DAL.DBHelpers
{
    public static class ConstructedToursDBHelper
    {
        private static ApplicationDbContext context;

        public static List<ConstructedTour> GetByUserId(string id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.ConstructedTours.Where(x => x.ApplicationUserId == id).ToList();
            }
        }

        public static long Add(ConstructViewModel model, string userId)
        {
            long? e1 = null;
            long? e2 = null;
            long? e3 = null;
            long? e4 = null;
            long? e5 = null;

            if (model.ExcursionsCount > 0)
            {
                e1 = model.Excursion1;
            }

            if (model.ExcursionsCount > 1)
            {
                e2 = model.Excursion2;
            }

            if (model.ExcursionsCount > 2)
            {
                e3 = model.Excursion3;
            }

            if (model.ExcursionsCount > 3)
            {
                e4 = model.Excursion4;
            }

            if (model.ExcursionsCount > 4)
            {
                e5 = model.Excursion5;
            }

            using (context = new ApplicationDbContext())
            {
                var newTour = new ConstructedTour()
                {
                    CityId = model.City,
                    CountryId = model.Country,
                    DestinationCityId = model.DestinationCity,
                    DestinationCountryId = model.DestinationCountry,
                    DestinationPointId = model.DestinationPoint,
                    TransportId = model.Transport,
                    RouteId = model.Route,
                    HotelId = model.Hotel,
                    LeavePointId = model.LeavePoint,
                    PeopleCount = model.PeopleCount,
                    ExcursionsCount = model.ExcursionsCount,
                    Excursion1Id = e1,
                    Excursion2Id = e2,
                    Excursion3Id = e3,
                    Excursion4Id = e4,
                    Excursion5Id = e5,
                    Class = model.Class,
                    HotelClass = model.HotelClass,
                    BackRouteId = model.BackRoute,
                    BackClass = model.BackClass,
                    ApplicationUserId = userId,
                    Price = model.Price 
                };

                context.ConstructedTours.Add(newTour);
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
                tour.Mark = mark;
                tour.NumberMark = numberMark;
                tour.Comment = comment;
                context.SaveChanges();
            }
        }
    }
}