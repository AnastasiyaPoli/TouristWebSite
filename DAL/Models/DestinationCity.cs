using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class DestinationCity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("DestinationCountry")]
        public long DestinationCountryId { get; set; }
        public DestinationCountry DestinationCountry { get; set; }
    }
}