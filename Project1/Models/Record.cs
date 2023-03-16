using Microsoft.Identity.Client;
using System.Text.RegularExpressions;

namespace Project1.Models
{
    public class Record
    {
        public Guid Id { get; set; }

        public int Price { get; set; }

        public int Capacity { get; set; }

        public Guid EventId { get; set; }
        public MassEvent? MassEvent { get; set; }   

        public Guid HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        public Guid GroupId { get; set; }
        public Groups? Group { get; set; }
    }
}
