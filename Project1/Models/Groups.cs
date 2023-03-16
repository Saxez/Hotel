using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class Groups
    {
        [Key]
        public Guid Id { get; set; }

        public int Count { get; set; }

        public string Name { get; set; }
        public User? Manager { get; set; }
        public Guid ManagerId { get; set; }

        public Guid MassEventId { get; set; } // внешний ключ
        public MassEvent? MassEvent { get; set; } // навигационное свойство

        public List<Record> Records { get; set; } = new();
    }
}
