using TecAlliance.Carpools.Data.Models;

namespace DataService
{
    public interface IUserDataService
    {
        User AddUser(User user);
        User DeleteUser(int userID, string password);
        List<User> GetAllUser();
        User GetUserByID(int userId);
        User UpdateUser(User user, string OldPassword);
    }
}