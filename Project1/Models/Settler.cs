using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class Settler
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Gender { get; set; }

        public int AdditionalPeoples { get; set; }

        [Required]
        public int GroupId { get; set; }

        public int preferred_type { get; set; }

    }
}
