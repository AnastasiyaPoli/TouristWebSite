using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Route
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("LeavePoint")]
        public long LeavePointId { get; set; }
        public LeavePoint LeavePoint { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}