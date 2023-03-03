using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class UserXHotel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
    }
}
