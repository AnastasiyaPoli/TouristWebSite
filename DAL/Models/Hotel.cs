using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Hotel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("DestinationPoint")]
        public long DestinationPointId { get; set; }
        public DestinationPoint DestinationPoint { get; set; }

        public int Price { get; set; }
    }
}