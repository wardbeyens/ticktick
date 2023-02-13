using MediatR;
using Microsoft.EntityFrameworkCore;
using TickTick.Api.Dtos;
using TickTick.Models;
using TickTick.Repositories.Base;

namespace TickTick.Api.RequestHandlers
{

    public class GetAllPersonsRequest : QueryBase<IEnumerable<PersonDto>>
    {}

    public class GetAllPersonsRequestHandler : IRequestHandler<GetAllPersonsRequest, IEnumerable<PersonDto>>
    {
        private readonly IRepository<Person> personsRepo;

        public GetAllPersonsRequestHandler(IRepository<Person> personsRepo)
        {
            this.personsRepo = personsRepo;
        }

        public async Task<IEnumerable<PersonDto>> Handle(GetAllPersonsRequest request, CancellationToken cancellationToken)
        {
            var persons = await personsRepo.GetAll().ToListAsync();
            var dto = new List<PersonDto>();

            foreach (var person in persons)
            {
                dto.Add(person.ConvertToDto());
            }
            return dto;
        }
    }
}
