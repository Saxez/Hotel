using Project1.Models;

namespace Project1.Database
{
    public class InitData
    {
        public InitData(AppDbContext db)
        {
            User User1 = new User { FirstName = "Tom", Email = "Tom@gmail.com", LastName = "Tomasson", Password = "1234", Role = "admin" };
            //User User2 = new User { FirstName = "John", Email = "John@gmail.com", LastName = "Johnson", Password = "4321", Role = "manager" };
            //User User3 = new User { FirstName = "Smith", Email = "Smith@gmail.com", LastName = "Smithson", Password = "1111", Role = "manager" };
            //User User4 = new User { FirstName = "Main", Email = "Main@gmail.com", LastName = "Mainson", Password = "1111", Role = "main manager" };
            //db.Users.AddRange(User1, User2, User3, User4);

            MassEvent MassEvent1 = new MassEvent { DateOfEnd = DateTime.UtcNow, DateOfStart = DateTime.UtcNow, Name = "Event 1", Description = "test desc" };
            //MassEvent MassEvent2 = new MassEvent { DateOfEnd = DateTime.UtcNow, DateOfStart = DateTime.UtcNow, Name = "Event 2", Description = "test desc2" };
            //db.MassEvents.AddRange(MassEvent1, MassEvent2);

            //Groups Group1 = new Groups { Count = 5, MassEvent = MassEvent2, Manager = User2, Name = "5 man group" };
            //Groups Group2 = new Groups { Count = 1, MassEvent = MassEvent2, Manager = User3, Name = "1 man group" };
            Groups UnallocatedSettlers = new Groups { Count = 0, MassEvent = MassEvent1, Manager = User1, Name = "Unallocated Settlers" };
            
            //db.Groups.AddRange(Group1, Group2, UnallocatedSettlers);

            //Hotel Hotel1 = new Hotel { Name = "Hotel 1", MassEvent = MassEvent1 };
            //Hotel Hotel2 = new Hotel { Name = "Hotel 2", MassEvent = MassEvent1 };
            //Hotel Hotel3 = new Hotel { Name = "Hotel 3", MassEvent = MassEvent1 };
            //Hotel Hotel4 = new Hotel { Name = "Hotel 4", MassEvent = MassEvent2 };
            //db.Hotels.AddRange(Hotel1, Hotel2, Hotel3, Hotel4);

            Settler settler = new Settler { FirstName = "Set", LastName = "Ler", Email = "12@gmail.com", Gender = 1, Groups = UnallocatedSettlers, PreferredType = "econom"};
            db.Settler.AddRange(settler);      

            db.SaveChanges();
        }
    }
}
