using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class Groups
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        public int MassEventId { get; set; } // внешний ключ
        public MassEvent? MassEvent { get; set; } // навигационное свойство
    }
}
