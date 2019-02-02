using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TouristWebSite.Models
{
    public enum MaritalStatus { Single = 0, Relationship = 1, Married = 2 }
    public enum Gender { Male = 0, Female = 1 }
    public enum Country { Ukraine = 0, Russia = 1, Belarus = 2, Moldova = 3, Poland = 4 }

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public bool IsSubscribed { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public Country Country { get; set; }
        public string City { get; set; }
        public string Profession { get; set; }
        public string Biography { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}