using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class User
    {
        //[Key]
        public Guid Id { get; set; }

        public string? First_name { get; set; }

        public string? Last_name { get; set; }

        public string? Email { get; set; }


        public string? Password { get; set; }

        public string? Role { get; set; }

        public List<UserXHotel> UserXHotels { get; set; } = new();
        public List<DifferenceDataHotel> DifferenceDataHotels { get; set; } = new();
        public List<EnteredDataHotel> EnteredDataHotel { get; set; } = new();
        public List<Groups> Groups { get; set; } = new();


    }


}
