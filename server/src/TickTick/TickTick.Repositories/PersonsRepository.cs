using TickTick.Models;
using TickTick.Repositories.Base;

namespace TickTick.Repositories
{
    // Je kan werken met een class of met een extension method (hier gebruiken we extension method (zie this))
    //public class PersonsRepository : Repository<Person>
    //{}

    public static class PersonsRepositoryExtension
    {

        public static IEnumerable<Person> GetDeadPeople(this Repository<Person> repo)
        {
            return repo.GetAll();
        }
    }
}
