using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.Data
{
    internal static class Repository
    {
        internal static List<Hotel> GetAllHotels()
        {
            using(var Db = new AppDbContext())
            {
                return Db.Hotels.Include(u => u.MassEvent).ToList();
            }
        }

        internal static List<User> GetAllUsers()
        {
            using (var Db = new AppDbContext())
            {
                return Db.Users.ToList();
            }
        }

        internal static List<MassEvent> GetAllEvents()
        {
            using (var Db = new AppDbContext())
            {
                return Db.MassEvents.ToList();
            }
        }

        internal static Groups GetUnallocatedGroup()
        {
            using (var Db = new AppDbContext())
            {
                return Db.Groups.ToList().FirstOrDefault(g => g.Name == "Unallocated Settlers");
            }
        }

    }
}
