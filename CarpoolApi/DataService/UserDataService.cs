using System.Data.SqlClient;
using TecAlliance.Carpools.Data.Models;

namespace DataService
{
    public class UserDataService : IUserDataService
    {
        public string connectionString = @"Data Source=localhost;Initial Catalog=CarpoolApp;Integrated Security=True;";

        public User GetUserByID(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"SELECT * FROM Users WHERE UserID = {userId}";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        return new User(Convert.ToInt32(reader["UserID"]),
                            reader["Password"].ToString(),
                            reader["Firstname"].ToString(),
                            reader["Lastname"].ToString(),
                            Convert.ToBoolean(reader["CanDrive"]),
                            Convert.ToBoolean(reader["Deleted"]));
                    }
                }
                finally
                {
                    reader.Close();
                }
                return null;
            }
        }

        public void AddUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"INSERT INTO Users(Password,Firstname,Lastname,CanDrive,Deleted)" +
                    $"VALUES('{user.Password}','{user.FirstName}','{user.LastName}','{user.CanDrive}','{user.Deleted}')";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.BeginExecuteNonQuery();
            }
        }

        public List<User> ReadUserData()
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
    }
}