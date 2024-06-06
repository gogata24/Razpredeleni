using BSCars2.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.ViewModel
{
    public class LikedCarViewModel
    {
        public int CarId { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfLiked { get; set; }
    }
}
