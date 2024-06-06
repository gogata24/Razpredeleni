using BSCars2.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Website.ViewModel
{
    public class CarViewModel
    {
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
        public int? UserId { get; set; }
        public string UserName {  get; set; }
        public string UserPhone {  get; set; }
    }
}
