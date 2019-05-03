using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TouristWebSite.Models;

namespace DAL.Models
{
    public class ConstructedTour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("Route")]
        public long RouteId { get; set; }
        public Route Route { get; set; }

        public string Class { get; set; }

        [ForeignKey("Hotel")]
        public long HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public long PeopleCount { get; set; }

        public string HotelClass { get; set; }

        [ForeignKey("BackRoute")]
        public long BackRouteId { get; set; }
        public BackRoute BackRoute { get; set; }

        public string BackClass { get; set; }

        [ForeignKey("User")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }

        public long Price { get; set; }

        public string Comment { get; set; }

        public string Mark { get; set; }

        public int NumberMark { get; set; }
    }
}