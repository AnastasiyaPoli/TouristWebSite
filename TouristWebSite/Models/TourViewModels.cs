using DAL.Models;
using System.Collections.Generic;

namespace TouristWebSite.Models
{
    public class ActiveToursViewModel
    {
        public List<Tour> ActiveTours { get; set; }
    }

    public class ChosenTourViewModel
    {
        public Tour ChosenTour { get; set; }
    }

    public partial class Image
    {
        public int ID { get; set; }
        public string ImagePath { get; set; }
    }
}
