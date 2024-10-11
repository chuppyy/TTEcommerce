namespace TTEcommerce.Domain.UserAggregate
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public int StatusId { get; set; }
    }
}
