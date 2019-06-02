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

        public string Link { get; set; }

        [ForeignKey("DestinationCity")]
        public long DestinationCityId { get; set; }
        public DestinationCity DestinationCity { get; set; }

        public long PriceStandart { get; set; }
        public long PriceLux { get; set; }
    }
}