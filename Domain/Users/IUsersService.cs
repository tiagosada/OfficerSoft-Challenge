using Domain.Common;

namespace Domain.Users
{
    public interface IUsersService : IService<User>
    {
        CreatedEntityDTO Create( string username, string password);
    }
}