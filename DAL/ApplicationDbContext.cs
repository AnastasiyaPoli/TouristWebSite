﻿using DAL.Models;
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
    }
}