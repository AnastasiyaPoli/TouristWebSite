using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Excursion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        [ForeignKey("Hotel")]
        public long HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public long Price { get; set; }
    }
}
