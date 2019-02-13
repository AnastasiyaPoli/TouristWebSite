using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class DestinationPoint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("DestinationCity")]
        public long DestinationCityId { get; set; }
        public DestinationCity DestinationCity { get; set; }

        [ForeignKey("Transport")]
        public long TransportId { get; set; }
        public Transport Transport { get; set; }
    }
}