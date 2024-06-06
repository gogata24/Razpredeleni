using System.ComponentModel.DataAnnotations;

namespace Website.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; } 
        [Required]
        [MaxLength(20)]
        public string Username { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
        [Required]
        [MaxLength(90)]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        [MaxLength(90)]
        public string City { get; set; }
        [MaxLength(20)]
        public string? NewField { get; set; }
    }
}
