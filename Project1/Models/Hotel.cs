using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class Hotel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]  
        public string? Name { get; set; }

        public string? Adress { get; set; }

        public string? Rules { get; set; }

        public TimeSpan CheckIn { get; set; }

        public TimeSpan CheckOut { get; set; }

        public int Stars { get; set; }

        [Required]
        public Guid MassEventId { get; set; } // внешний ключ
        public MassEvent? MassEvent { get; set; } // навигационное свойство

        public List<EnteredDataHotel> EnteredDataHotels { get; set; } = new();

        public List<RecordDataHotel> RecordDataHotels { get; set; } = new();

        public List<DifferenceDataHotel> DifferenceDataHotels { get; set; } = new();

        public List<UserXHotel> UserXHotels { get; set; } = new();

        public List<Record> Records { get; set; } = new();
    }
}
