using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class MassEvent
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DateOfStart { get; set; }

        [Required]
        public DateTime DateOfEnd { get; set; }


        public List<Hotel> Hotels { get; set; } = new();

        public List<Groups> Groups { get; set; } = new();

        public List<EnteredDataHotel> EnteredDataHotels { get; set; } = new();

        public List<RecordDataHotel> RecordDataHotels { get; set; } = new();

        public List<DifferenceDataHotel> DifferenceDataHotels { get; set; } = new();
    }
}
