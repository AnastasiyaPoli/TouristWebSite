using System;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;
using TouristWebSite.Models;

namespace DAL.DBHelpers
{
    public static class ToursDBHelper
    {
        /// <summary>
        /// Database context
        /// </summary>
        private static ApplicationDbContext context;

        /// <summary>
        /// Getting active tours
        /// </summary>
        /// <returns> List of active tours </returns>
        public static List<Tour> GetActive()
        {
            using (context = new ApplicationDbContext())
            {
                return context.Tours.Where(x => x.IsActive && x.DateStart >= DateTime.Now).ToList();
            }
        }

        /// <summary>
        /// Getting tour by id
        /// </summary>
        /// <param name="id"> Tour id </param>
        /// <returns> Tour </returns>
        public static Tour GetById(long id)
        {
            using (context = new ApplicationDbContext())
            {
                return context.Tours.FirstOrDefault(x => x.Id == id);
            }
        }

        /// <summary>
        /// Adding new tour
        /// </summary>
        /// <param name="tour"> Tour view model </param>
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

        /// <summary>
        /// Updating tour
        /// </summary>
        /// <param name="tour"> Tour view model </param>
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

        /// <summary>
        /// Deactivating tour
        /// </summary>
        /// <param name="id"> Tour id </param>
        public static void Deactivate(long id)
        {
            using (context = new ApplicationDbContext())
            {
                context.Tours.FirstOrDefault(x => x.Id == id).IsActive = false;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Adding new photos for tour
        /// </summary>
        /// <param name="id"> Tour id </param>
        /// <param name="number"> Number of photots </param>
        public static void AddNewPhotos(long id, int number)
        {
            using (context = new ApplicationDbContext())
            {
                context.Tours.FirstOrDefault(x => x.Id == id).NumberOfPhotos = context.Tours.FirstOrDefault(x => x.Id == id).NumberOfPhotos + number;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deleting all photos for tour
        /// </summary>
        /// <param name="id"> Tour id </param>
        public static void DeleteAllPhotos(long id)
        {
            using (context = new ApplicationDbContext())
            {
                context.Tours.FirstOrDefault(x => x.Id == id).NumberOfPhotos = 0;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deleting photo for tour
        /// </summary>
        /// <param name="id"> Tour id </param>
        public static void DeletePhoto(long id)
        {
            using (context = new ApplicationDbContext())
            {
                context.Tours.FirstOrDefault(x => x.Id == id).NumberOfPhotos = context.Tours.FirstOrDefault(x => x.Id == id).NumberOfPhotos - 1;
                context.SaveChanges();
            }
        }

    }
}