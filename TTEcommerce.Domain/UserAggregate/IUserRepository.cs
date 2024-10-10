using System.Threading.Tasks;

namespace TTEcommerce.Domain.UserAggregate
{
    public interface IUserRepository
    {
        Task<UserDto> GetUserByIdAsync(string id);
    }
}
