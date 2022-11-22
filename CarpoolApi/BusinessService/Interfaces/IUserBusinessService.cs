using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Business.Interfaces
{
    public interface IUserBusinessService
    {
        UserDto ChangeUserData(User user, string OldPassword);
        UserDto ConvertUserToDto(User user);
        UserDto CreateNewUser(string password, string firstname, string lastname, bool canDrive);
        UserDto DeleteUserByID(int userID, string password);
        List<UserDto> GetAllUser();
        UserDto GetUserByID(int userID);
    }
}