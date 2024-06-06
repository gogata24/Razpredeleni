using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSCars2.Entities
{
    public class Cars
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Brand { get; set; }
        [Required]
        [MaxLength(20)]
        public string Model { get; set; }
        [Required]
        [MaxLength(4)]
        public string Year { get; set; }
        public double Price { get; set; }
        [Required]
        [MaxLength(90)]
        public string Description { get; set; }
        [ForeignKey("UserId")]
        public virtual Users? User { get; set; }
        public int? UserId { get; set; }
    }
}
