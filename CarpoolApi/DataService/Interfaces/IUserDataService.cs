using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Data.Interfaces
{
    public interface IUserDataService
    {
        void AddUser(User user);
        User DeleteUser(int userID, string password);
        List<User> GetAllUser();
        User GetNewestUser();
        User GetUserByID(int userId);
        User UpdateUser(User user, string OldPassword);
    }
}