using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Project1.Models
{
    public class Settler
    {
        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Gender { get; set; }

        public int AdditionalPeoples { get; set; }

        public Guid GroupsId { get; set; }

        public Groups? Groups { get; set; }

        public string PreferredType { get; set; }

    }
}
