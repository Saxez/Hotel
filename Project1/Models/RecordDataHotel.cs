using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class RecordDataHotel
    {
        [Key]
        public int Id { get; set; }

        public int Type { get; set; }

        public int Capacity { get; set; }

        public int Price { get; set; }

        [Required]
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        [Required]
        public int MassEventId { get; set; } // внешний ключ
        public MassEvent? MassEvent { get; set; } // навигационное свойство

        [Required]
        public int UserId { get; set; }

        [Required]
        public int GroupId { get; set; }

    }
}
