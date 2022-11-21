using TecAlliance.Carpools.Data.Models;

namespace DataService
{
    public interface IUserDataService
    {
        void AddUser(User user);
        User GetUserByID(int userId);
        List<User> ReadUserData();
    }
}