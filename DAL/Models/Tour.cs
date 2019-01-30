using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Tour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Place { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public long Price { get; set; }

        public int NumberOfPhotos { get; set; }

        public string RouteLink { get; set; }
    }
}