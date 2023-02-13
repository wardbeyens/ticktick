using MediatR;
using TickTick.Api.Dtos;
using TickTick.Models;
using TickTick.Repositories.Base;

namespace TickTick.Api.RequestHandlers
{

    public class GetPersonRequest : QueryBase<PersonDto>
    {
        public Guid PublicId { get; set; }
        public GetPersonRequest(Guid publicId)
        {
            PublicId = publicId;
        }
    }

    public class GetPersonRequestHandler : IRequestHandler<GetPersonRequest, PersonDto>
    {
        private readonly IRepository<Person> personRepository;

        public GetPersonRequestHandler(IRepository<Person> personRepository)
        {
            this.personRepository = personRepository;
        }

        public async Task<PersonDto> Handle(GetPersonRequest request, CancellationToken cancellationToken)
        {
            var p = await personRepository.GetAsync(p => p.PublicId == request.PublicId);
            return p.ConvertToDto();
        }
    }
}
