using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSCars2.Entities
{
    public class LikedCars
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("CarId")]
        public virtual Cars? Car { get; set; }

        public int CarId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users? User { get; set; }
        public int? UserId { get; set; }
        public DateTime DateOfLiked { get; set; }
    }
}
