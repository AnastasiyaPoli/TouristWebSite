using DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TouristWebSite.Models;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<New> News { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<BookedTour> BookedTours { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Favourite> Favourites { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<LeavePoint> LeavePoints { get; set; }
        public DbSet<DestinationCountry> DestinationCountries { get; set; }
        public DbSet<DestinationCity> DestinationCities { get; set; }
        public DbSet<DestinationPoint> DestinationPoints { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Excursion> Excursions { get; set; }
        public DbSet<BackRoute> BackRoutes { get; set; }
        public DbSet<ConstructedTour> ConstructedTours { get; set; }
    }
}