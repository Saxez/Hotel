using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class EnteredDataHotel
    {
        [Key]
        public Guid Id { get; set; }

        public int Type { get; set; }

        public int Сapacity { get; set; }

        [Required]
        public Guid HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        public int Price { get; set; }

        [Required]
        public Guid MassEventId { get; set; } // внешний ключ
        public MassEvent? MassEvent { get; set; } // навигационное свойство

        [Required]
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
