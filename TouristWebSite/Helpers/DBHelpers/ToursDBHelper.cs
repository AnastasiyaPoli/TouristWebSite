using System;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;
using TouristWebSite.Models;

namespace DAL.DBHelpers
{
    public static class ToursDBHelper
    {
        private static ApplicationDbContext context;

        public static List<Tour> GetActive()
        {
            using (context = new ApplicationDbContext())
            {
                return context.Tours.Where(x => x.IsActive && x.DateStart > DateTime.Now).ToList();
            }
        }

        public static Tour GetById(long id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.Tours.FirstOrDefault(x => x.IsActive && x.Id == id);
            }
        }

        public static void Deactivate(long id)
        {
            using (context = new ApplicationDbContext())
            {
                context.Tours.FirstOrDefault(x => x.Id == id).IsActive = false;
                context.SaveChanges();
            }
        }

        public static void Add(TourViewModel tour)
        {
            using (context = new ApplicationDbContext())
            {
                var newTour = new Tour()
                {
                    Name = tour.Name,
                    Place = tour.Place,
                    Description = tour.Description,
                    DateStart = tour.DateStart,
                    DateEnd = tour.DateEnd,
                    Price = tour.Price,
                    RouteLink = tour.RouteLink,
                    IsActive = true,
                    NumberOfPhotos = 0
                };

                context.Tours.Add(newTour);
                context.SaveChanges();
            }
        }

        public static void Update(TourViewModel tour)
        {
            using (context = new ApplicationDbContext())
            {
                var tourForUpdating = context.Tours.FirstOrDefault(x => x.Id == tour.Id);

                tourForUpdating.Name = tour.Name;
                tourForUpdating.Place = tour.Place;
                tourForUpdating.Description = tour.Description;
                tourForUpdating.DateStart = tour.DateStart;
                tourForUpdating.DateEnd = tour.DateEnd;
                tourForUpdating.Price = tour.Price;
                tourForUpdating.RouteLink = tour.RouteLink;

                context.SaveChanges();
            }
        }

        public static void DeleteAllPhotos(long id)
        {
            using (context = new ApplicationDbContext())
            {
                context.Tours.FirstOrDefault(x => x.Id == id).NumberOfPhotos = 0;
                context.SaveChanges();
            }
        }

        public static void AddNewPhoto(long id)
        {
            using (context = new ApplicationDbContext())
            {
                context.Tours.FirstOrDefault(x => x.Id == id).NumberOfPhotos = context.Tours.FirstOrDefault(x => x.Id == id).NumberOfPhotos + 1;
                context.SaveChanges();
            }
        }
    }
}