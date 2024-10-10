using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TTEcommerce.Domain.Core;

namespace TTEcommerce.Domain.UserAggregate
{
    public class User : Entity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int StatusId { get; set; }
    }
}
