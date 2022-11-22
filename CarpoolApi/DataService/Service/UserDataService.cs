using System.Data.SqlClient;
using TecAlliance.Carpools.Data.Interfaces;
using TecAlliance.Carpools.Data.Models;

namespace DataService
{
    public class UserDataService : IUserDataService
    {
        private string connectionString = @"Data Source=localhost;Initial Catalog=CarpoolApp;Integrated Security=True;";

        public void AddUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"INSERT INTO Users(Password,Firstname,Lastname,CanDrive,Deleted,ModifiedDate)" +
                    $"VALUES('{user.Password}','{user.FirstName}','{user.LastName}','{Convert.ToInt32(user.CanDrive)}','{Convert.ToInt32(false)}',GETDATE())";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.BeginExecuteNonQuery();
            }
        }

        public User GetNewestUser()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"SELECT TOP(1) * FROM Users ORDER BY ModifiedDate DESC";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var user = new User(Convert.ToInt32(reader["UserID"]),
                        reader["Password"].ToString(),
                        reader["Firstname"].ToString(),
                        reader["Lastname"].ToString(),
                        Convert.ToBoolean(reader["CanDrive"]),
                        Convert.ToBoolean(reader["Deleted"]));
                    reader.Close();
                    return user;
                }
                return null;
            }
        }

        public List<User> GetAllUser()
        {
            var users = new List<User>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM Users";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        users.Add(new User(Convert.ToInt32(reader["UserID"]),
                            reader["Password"].ToString(),
                            reader["Firstname"].ToString(),
                            reader["Lastname"].ToString(),
                            Convert.ToBoolean(reader["CanDrive"]),
                            Convert.ToBoolean(reader["Deleted"])));
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return users;
        }

        public User GetUserByID(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"SELECT * FROM Users WHERE UserID = {userId}";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var user = new User(Convert.ToInt32(reader["UserID"]),
                        reader["Password"].ToString(),
                        reader["Firstname"].ToString(),
                        reader["Lastname"].ToString(),
                        Convert.ToBoolean(reader["CanDrive"]),
                        Convert.ToBoolean(reader["Deleted"]));
                    reader.Close();
                    return user;
                }
                return null;
            }
        }

        public User UpdateUser(User user, string OldPassword)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (GetUserByID(user.UserID).Password == OldPassword)
                {
                    string queryString = $"UPDATE Users SET Password = '{user.Password}', " +
                        $"Firstname = '{user.FirstName}', " +
                        $"Lastname = '{user.LastName}', " +
                        $"CanDrive = {Convert.ToInt32(user.CanDrive)} " +
                        $"ModifiedDate = GETDATE()" +
                        $"WHERE UserID = {user.UserID}";
                    SqlCommand cmd = new SqlCommand(queryString, connection);
                    connection.Open();
                    cmd.BeginExecuteNonQuery();
                }
            }
            return GetUserByID(user.UserID);
        }

        public User DeleteUser(int userID, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var userToDelete = GetUserByID(userID);
                if (userToDelete.Password == password)
                {
                    string queryString = $"UPDATE Users SET Deleted = {!userToDelete.Deleted} WHERE UserID = {userID}" +
                        $"UPDATE CarpoolPassengers SET Deleted = 1 WHERE PassengerID = {userID}" +
                        $"UPDATE Carpools SET CarpoolDriverID = null, WHERE CarpoolDriverID = {userID}";

                    SqlCommand cmd = new SqlCommand(queryString, connection);
                    connection.Open();
                    cmd.BeginExecuteNonQuery();
                }
            }
            return GetUserByID(userID);
        }
    }
}