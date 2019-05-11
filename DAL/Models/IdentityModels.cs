using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TouristWebSite.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public bool IsSubscribed { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Profession { get; set; }
        public string Biography { get; set; }
        public bool IsActive { get; set; }
        public Guid? EmailGuid { get; set; }

        public string TypeOfStudies { get; set; }
        public string StudiesDescription { get; set; }
        public string TypeOfWork { get; set; }
        public string WorkDescription { get; set; }
        public string SportsDescription { get; set; }
        public string MusicDescription { get; set; }
        public string FilmsDescription { get; set; }
        public string BooksDescription { get; set; }
        public string HobbiesDescription { get; set; }
        public string PetsDescription { get; set; }
        public string OtherInfo { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}