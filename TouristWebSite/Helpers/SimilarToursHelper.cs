using DAL.DBHelpers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TouristWebSite.Helpers
{
    public class SimilarToursHelper
    {
        public static List<Tour> FindSimilarTours(Tour bookedTour, string userId)
        {
            var places = bookedTour.Place.Split(',').ToList();
            var tours = ToursDBHelper.GetActive().Where(x =>
                (x.Id != bookedTour.Id) &&
                (CommentsDBHelper.GetActiveForTour(x.Id).FirstOrDefault(n => n.ApplicationUserId == userId && (n.NumberMark != 0 && n.NumberMark < 4)) == null) &&
                (places.Any(s => x.Place.Contains(s))) &&
                (Math.Abs(x.Price - bookedTour.Price) <= 10000) &&
                (Math.Abs(((bookedTour.DateStart.Year - x.DateStart.Year) * 12) + bookedTour.DateStart.Month - x.DateStart.Month) <= 2)
            ).ToList();

            if (tours.Count > 3)
            {
                var listOfToursWithMarks = tours.Select(k =>
                    {
                        var commentsWithMarks = CommentsDBHelper.GetActiveForTour(k.Id)
                            .Where(b => b.NumberMark != 0);

                        return new
                        {
                            Tour = k,
                            Mark = (double)commentsWithMarks.Count() != 0
                                ? (double)commentsWithMarks.Sum(y => y.NumberMark) /
                                  ((double)commentsWithMarks.Count())
                                : 0
                        };
                    })
                    .OrderByDescending(m => m.Mark)
                    .Take(3)
                    .ToList();

                tours = new List<Tour>()
                {
                    listOfToursWithMarks[0].Tour,
                    listOfToursWithMarks[1].Tour,
                    listOfToursWithMarks[2].Tour
                };
            }

            return tours;
        }
    }
}