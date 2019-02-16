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

        [ForeignKey("Country")]
        public long CountryId { get; set; }
        public Country Country { get; set; }

        [ForeignKey("City")]
        public long CityId { get; set; }
        public Country City { get; set; }

        [ForeignKey("Transport")]
        public long TransportId { get; set; }
        public Transport Transport { get; set; }

        [ForeignKey("LeavePoint")]
        public long LeavePointId { get; set; }
        public LeavePoint LeavePoint { get; set; }

        [ForeignKey("DestinationCountry")]
        public long DestinationCountryId { get; set; }
        public DestinationCountry DestinationCountry { get; set; }

        [ForeignKey("DestinationCity")]
        public long DestinationCityId { get; set; }
        public DestinationCity DestinationCity { get; set; }

        [ForeignKey("DestinationPoint")]
        public long DestinationPointId { get; set; }
        public DestinationPoint DestinationPoint { get; set; }

        [ForeignKey("Route")]
        public long RouteId { get; set; }
        public Route Route { get; set; }

        public string Class { get; set; }

        [ForeignKey("Hotel")]
        public long HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public long PeopleCount { get; set; }

        public string HotelClass { get; set; }

        public long ExcursionsCount { get; set; }

        [ForeignKey("Excursion1")]
        public long? Excursion1Id { get; set; }
        public Excursion Excursion1 { get; set; }

        [ForeignKey("Excursion2")]
        public long? Excursion2Id { get; set; }
        public Excursion Excursion2 { get; set; }

        [ForeignKey("Excursion3")]
        public long? Excursion3Id { get; set; }
        public Excursion Excursion3 { get; set; }

        [ForeignKey("Excursion4")]
        public long? Excursion4Id { get; set; }
        public Excursion Excursion4 { get; set; }

        [ForeignKey("Excursion5")]
        public long? Excursion5Id { get; set; }
        public Excursion Excursion5 { get; set; }

        [ForeignKey("BackRoute")]
        public long BackRouteId { get; set; }
        public BackRoute BackRoute { get; set; }

        public string BackClass { get; set; }

        [ForeignKey("User")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}