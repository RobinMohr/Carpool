using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TecAlliance.Carpools.Data.Interfaces;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Data.Service
{
    public class CarpoolDataService : ICarpoolDataService
    {
        public string connectionString = @"Data Source=localhost;Initial Catalog=CarpoolApp;Integrated Security=True;";

        IUserDataService _userDataService;

        public CarpoolDataService(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public Carpool GetCarpoolByID(int carpoolID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"SELECT * FROM Carpools WHERE CarpoolID = {carpoolID}";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Carpool carpool = GetCarpool(reader);
                    return carpool;
                }
                return null;
            }
        }

        private Carpool GetCarpool(SqlDataReader reader)
        {
            int id = (int)reader["CarpoolID"];

            User driver = null;
            if (!reader.IsDBNull("CarpoolDriverID"))
            {
                driver = _userDataService.GetUserByID((int)reader["CarpoolDriverID"]);
            }
            return new Carpool(
                id,
                reader["Password"].ToString(),
                driver,
                reader["Origin"].ToString(),
                reader["Destination"].ToString(),
                (int)reader["FreeSpaces"],
                AllPassengersInCarpool(id),
                reader["DepartmentTime"].ToString(),
                Convert.ToBoolean(reader["Deleted"]));            
        }

        public List<Carpool> GetAllCarpools()
        {
            List<Carpool> allCarpools = new List<Carpool>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM Carpools";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    allCarpools.Add(GetCarpool(reader));
                }
            }
            return allCarpools;
        }

        public List<User> AllPassengersInCarpool(int carpoolID)
        {
            var users = new List<User>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"SELECT * FROM CarpoolPassengers WHERE CarpoolID = {carpoolID}";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        users.Add(_userDataService.GetUserByID((int)reader["PassengerID"]));
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return users;
        }

        public Carpool ChangeCarpoolDataByID(Carpool newCarpoolData)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //if ()
                //{
                //    string queryString = $"UPDATE Users SET Password = '{user.Password}', " +
                //        $"Firstname = '{user.FirstName}', " +
                //        $"Lastname = '{user.LastName}', " +
                //        $"CanDrive = {Convert.ToInt32(user.CanDrive)} " +
                //        $"ModifiedDate = GETDATE()" +
                //        $"WHERE UserID = {user.UserID}";
                //    SqlCommand cmd = new SqlCommand(queryString, connection);
                //    connection.Open();
                //    cmd.BeginExecuteNonQuery();
                //}
                //else
                //{
                //    return null;
                //}
            }
            return GetCarpoolByID(newCarpoolData.CarpoolId);
        }
    }
}
