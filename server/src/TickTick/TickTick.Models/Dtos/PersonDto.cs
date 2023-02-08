namespace TickTick.Models.Dtos
{
    public class PersonDto
    {
        public Guid PublicId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public IList<LocationDto> Addresses { get; set; }
    }
}
