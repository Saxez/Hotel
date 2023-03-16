using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class RecordDataHotel
    {
        [Key]
        public Guid Id { get; set; }

        public int Type { get; set; }

        public int Capacity { get; set; }

        public int Price { get; set; }

        [Required]
        public Guid HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        [Required]
        public Guid MassEventId { get; set; } // внешний ключ
        public MassEvent? MassEvent { get; set; } // навигационное свойство

        [Required]
        public Guid UserId { get; set; }
        public User? User { get; set; }

        [Required]
        public Guid GroupId { get; set; }
        public Groups? Groups { get; set; }

    }
}
