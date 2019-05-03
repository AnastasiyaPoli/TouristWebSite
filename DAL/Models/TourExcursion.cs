using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class TourExcursion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("Excursion")]
        public long ExcursionId { get; set; }
        public Excursion Excursion { get; set; }

        [ForeignKey("ConstructedTour")]
        public long ConstructedTourId { get; set; }
        public ConstructedTour ConstructedTour { get; set; }
    }
}
