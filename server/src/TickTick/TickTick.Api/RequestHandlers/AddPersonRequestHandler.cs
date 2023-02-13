using MediatR;
using System;
using TickTick.Api.Dtos;
using TickTick.Api.Services;
using TickTick.Models;
using TickTick.Repositories.Base;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TickTick.Api.RequestHandlers
{
    
    public class AddPersonRequest : CommandBase<PersonDto>
    {
        public AddPersonDto ToAddPersonDto { get; set; }

        public AddPersonRequest(AddPersonDto personDto)
        {
            ToAddPersonDto = personDto;
        }
    }

    public class AddPersonRequestHandler : IRequestHandler<AddPersonRequest, PersonDto>
    {
        private readonly IRepository<Person> repo;

        public AddPersonRequestHandler(IRepository<Person> personRepository)
        {
            this.repo = personRepository;
        }

        public async Task<PersonDto> Handle(AddPersonRequest request, CancellationToken cancellationToken)
        {
            Person p = new Person(request.ToAddPersonDto.FirstName, request.ToAddPersonDto.LastName, request.ToAddPersonDto.Email);
            p.CreatePublicId();
            repo.Add(p);
            int i = await repo.SaveAsync();
            System.Diagnostics.Debug.WriteLine("Add Person:" + i + " : " + p.ToString());
            return p.ConvertToDto();
        }
    }
}
