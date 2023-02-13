using MediatR;
using TickTick.Api.Dtos;
using TickTick.Models;
using TickTick.Repositories.Base;

namespace TickTick.Api.RequestHandlers
{
    public class DeletePersonRequest : CommandBase<PersonDto>
    {
        public Guid PublicId { get; set; }

        public DeletePersonRequest(Guid publicId)
        {
            PublicId = publicId;
        }
    }

    public class DeletePersonRequestHandler : IRequestHandler<DeletePersonRequest, PersonDto>
    {
        private readonly IRepository<Person> repo;

        public DeletePersonRequestHandler(IRepository<Person> personRepository)
        {
            this.repo = personRepository;



        }

        public async Task<PersonDto> Handle(DeletePersonRequest request, CancellationToken cancellationToken)
        {

            var p = await repo.GetAsync(p => p.PublicId == request.PublicId);

            if (p == null)
            {
                throw new Exception("Person not found");
            }

            p.Delete();
            repo.Delete(p);

            int i = await repo.SaveAsync();
            return p.ConvertToDto();
        }
    }
}
