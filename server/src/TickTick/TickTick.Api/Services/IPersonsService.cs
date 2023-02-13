using TickTick.Api.Dtos;
using TickTick.Models;

namespace TickTick.Api.Services
{
    public interface IPersonsService
    {
        void DeletePerson(Guid id);
        void DeletePerson(Person person);
        PersonDto AddPerson(AddPersonDto dto);
        PersonDto UpdatePerson(Guid personId, PersonDto dto);
    }
}