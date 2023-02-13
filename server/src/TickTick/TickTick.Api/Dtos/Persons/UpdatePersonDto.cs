namespace TickTick.Api.Dtos
{
    public class UpdatePersonDto
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }


        public DateTime? DateOfBirth { get; set; }
        public string? MiddleName { get; set; }
    }
}
