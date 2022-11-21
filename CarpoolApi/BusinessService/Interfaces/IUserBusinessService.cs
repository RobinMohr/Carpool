using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Business.Interfaces
{
    public interface IUserBusinessService
    {
        UserDto ConvertUserToDto(User user);
        List<UserDto> GetAllUser();
        UserDto GetUserByID(int userID);
    }
}