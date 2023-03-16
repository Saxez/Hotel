using System;
using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class UserXHotel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public User? User { get; set; }

        [Required]
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
    }
}
