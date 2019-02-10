using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class LeavePoint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("City")]
        public long CityId { get; set; }
        public Country City { get; set; }

        [ForeignKey("Transport")]
        public long TransportId { get; set; }
        public Transport Transport { get; set; }
    }
}