using System.Data;
using System.Data.SqlClient;
using TecAlliance.Carpools.Data.Models;

namespace DataService
{
    public class UserDataService : IUserDataService
    {
        private string connectionString = @"Data Source=localhost;Initial Catalog=CarpoolApp;Integrated Security=True;";

        public User AddUser(User user)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            string queryString = $"INSERT INTO Users(Password,Firstname,Lastname,CanDrive) " +
                                     $"OUTPUT inserted.UserID " +
                $"VALUES(@UserPassword,@FirstName,@LastName,@CanDrive)";
            SqlCommand command = new SqlCommand(queryString, connection);

            command.Parameters.Add("@UserPassword", SqlDbType.NVarChar);
            command.Parameters["@UserPassword"].Value = user.Password;

            command.Parameters.Add("@FirstName", SqlDbType.VarChar);
            command.Parameters["@FirstName"].Value = user.FirstName;

            command.Parameters.Add("@LastName", SqlDbType.NVarChar);
            command.Parameters["@LastName"].Value = user.LastName;

            command.Parameters.Add("@CanDrive", SqlDbType.Bit);
            command.Parameters["@CanDrive"].Value = Convert.ToInt32(user.CanDrive);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                user.UserID = (int)reader["UserID"];
                reader.Close();
                return user;
            }
            connection.Close();
            return null;
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
                        users.Add(GetUser(reader));
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
                string queryString = $"SELECT * FROM Users WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@UserID", SqlDbType.Int);
                command.Parameters["@UserID"].Value = userId;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    User user = GetUser(reader);
                    reader.Close();
                    return user;
                }
                return null;
            }
        }

        private static User GetUser(SqlDataReader reader)
        {
            return new User(Convert.ToInt32(reader["UserID"]),
                reader["Password"].ToString(),
                reader["Firstname"].ToString(),
                reader["Lastname"].ToString(),
                Convert.ToBoolean(reader["CanDrive"]));
        }

        public User UpdateUser(User user, string OldPassword)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (GetUserByID(user.UserID).Password == OldPassword)
                {
                    string queryString =
                        $"UPDATE Users SET " +
                        $"Password = @Password, " +
                        $"Firstname = @FirstName, " +
                        $"Lastname = @LastName, " +
                        $"CanDrive = @CanDrive " +
                        $"WHERE UserID = @UserID";
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.Add("@UserID", SqlDbType.Int);
                    command.Parameters["@UserID"].Value = user.UserID;

                    command.Parameters.Add("@Password", SqlDbType.NVarChar);
                    command.Parameters["@Password"].Value = user.Password;

                    command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                    command.Parameters["@FirstName"].Value = user.FirstName;

                    command.Parameters.Add("@LastName", SqlDbType.VarChar);
                    command.Parameters["@LastName"].Value = user.LastName;

                    command.Parameters.Add("@CanDrive", SqlDbType.Bit);
                    command.Parameters["@CanDrive"].Value = user.CanDrive;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                else
                {
                    return null;
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
                    string queryString =
                        $"DELETE FROM Users WHERE UserID = @UserID" +
                        $"DELETE FROM Carpools WHERE CarpoolDriverID = @UserID" +
                        $"DELETE FROM CarpoolPassengers WHERE PassengerID = @UserID";

                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.Add("@UserID", SqlDbType.Int);
                    command.Parameters["@UserID"].Value = userID;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return GetUserByID(userID);
        }
    }
}