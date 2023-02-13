﻿using TickTick.Api.Dtos;

namespace TickTick.Api.Services
{
    public interface IPersonsService
    {
        void DeletePerson(Guid id);
        PersonDto AddPerson(AddPersonDto dto);
        PersonDto UpdatePerson(Guid personId, PersonDto dto);
    }
}