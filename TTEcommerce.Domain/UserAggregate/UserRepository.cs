using System.Threading.Tasks;
using TTEcommerce.Domain.Core;

namespace TTEcommerce.Domain.UserAggregate
{
    public class UserRepository : IUserRepository
    {
        private readonly IDapperRepository<User> _dapperRepository;
        private readonly IRepository<User> _repository;

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await _dapperRepository.QueryFirstOrDefaultAsync(
                @"SELECT u.Id, u.FirstName, u.LastName, u.Code, u.Email, u.StatusId
                  FROM Users u 
                  WHERE u.Id = @userId AND u.IsDeleted = 0",
                new { userId });

            if (user != null)
            {
                return new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Code = user.Code,
                    StatusId = user.StatusId
                };
            }else 
            { 
                return new UserDto();
            }
            
        }
    }
}
