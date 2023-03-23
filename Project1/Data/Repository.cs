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

        internal static User GetUserByEmailAndPassword(string email, string password)
        {
            using (var Db = new AppDbContext())
            {
                return Db.Users.ToList().FirstOrDefault(p => p.Email == email && p.Password == password);
            }
        }

        internal static List<Groups> GetGroupsByOwnerId(string id)
        {
            using (var Db = new AppDbContext())
            {
                return Db.Groups.Include(group => group.Manager).Where(group => group!.Manager.Id.ToString() == id).ToList();
            }
        }

        internal static void AddSettler(string FirstName, string LastName, int AdditionalPeople, string Email, string PrefferedType)
        {
            using (var Db = new AppDbContext())
            {
                Groups UnallocatedGroup = Db.Groups.ToList().FirstOrDefault(g => g.Name == "Unallocated Settlers");
                Settler settler = new Settler { FirstName = FirstName, LastName = LastName, AdditionalPeoples = AdditionalPeople, Email = Email, PreferredType = PrefferedType, Groups = UnallocatedGroup };
                Db.Settler.Add(settler);
                Db.SaveChanges();
            }
        }
    }
}
