using MediatR;
using TickTick.Api.Dtos;
using TickTick.Models;
using TickTick.Repositories.Base;

namespace TickTick.Api.RequestHandlers
{

    public class UpdatePersonRequest : CommandBase<PersonDto>
    {
        public Guid PublicId { get; set; }
        public UpdatePersonDto ToUpdatePersonDto { get; set; }

        public UpdatePersonRequest(Guid publicId, UpdatePersonDto updatePersonDto)
        {
            PublicId = publicId;
            ToUpdatePersonDto = updatePersonDto;
        }
    }
    public class UpdatePersonRequestHandler : IRequestHandler<UpdatePersonRequest, PersonDto>
    {

        private readonly IRepository<Person> repo;

        public UpdatePersonRequestHandler(IRepository<Person> personRepository)
        {
            this.repo = personRepository;
        }

        public async Task<PersonDto> Handle(UpdatePersonRequest request, CancellationToken cancellationToken)
        {
            var p = await repo.GetAsync(p => p.PublicId == request.PublicId);

            if (p == null)
            {
                throw new Exception("Person not found");
            }

            p.Update(request.ToUpdatePersonDto.FirstName, request.ToUpdatePersonDto.LastName, request.ToUpdatePersonDto.Email, request.ToUpdatePersonDto.DateOfBirth, request.ToUpdatePersonDto.MiddleName);

            repo.Update(p);
            int i = await repo.SaveAsync();
            return p.ConvertToDto();
        }
    }
}
