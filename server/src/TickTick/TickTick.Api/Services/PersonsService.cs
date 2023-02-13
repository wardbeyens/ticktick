﻿using TickTick.Api.Dtos;
using TickTick.Models;

namespace TickTick.Api.Services
{
    public class PersonsService : IPersonsService
    {
        public void DeletePerson(Guid id)
        {
            //TODO: persoon ophalen
            // als persoon null is, 404 teruggeven
            // p.Delete()
            // save            
        }

        public void DeletePerson(Person person)
        {
            person.Delete();
            //TODO: persoon ophalen
            // als persoon null is, 404 teruggeven
            // p.Delete()
            // save            
        }

        public PersonDto AddPerson(AddPersonDto dto)
        {
            Person person = new Person(
                dto.FirstName,
                dto.LastName,
                dto.Email);

            person.CreatePublicId();
            return person.ConvertToDto();
            
        }
        
        public PersonDto UpdatePerson(Guid personId, PersonDto dto) {
            //TODO: use a person from the database
            Person person = new Person(
                dto.FirstName,
                dto.LastName,
                dto.Email);
            person.Update(dto.FirstName, dto.LastName, dto.MiddleName, dto.DateOfBirth, dto.Email);
            return person.ConvertToDto();
        }
    }
}
