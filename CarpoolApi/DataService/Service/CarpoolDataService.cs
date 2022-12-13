using DataService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Data.Service
{
    public class CarpoolDataService : ICarpoolDataService
    {
        public string connectionString = @"Data Source=localhost;Initial Catalog=CarpoolApp;Integrated Security=True;";
        readonly IUserDataService _userDataService;

        public CarpoolDataService(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public Carpool GetCarpoolByID(int carpoolID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"SELECT * FROM Carpools WHERE CarpoolID = ";
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@CarpoolID", SqlDbType.Int);
                command.Parameters["@CarpoolID"].Value = carpoolID;

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
                Convert.ToDateTime(reader["DepartmentTime"]));
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

                while (reader.Read())
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
                string queryString = $"SELECT * FROM CarpoolPassengers WHERE CarpoolID = @CarpoolID";
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@CarpoolID", SqlDbType.Int);
                command.Parameters["@CarpoolID"].Value = carpoolID;

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

        public Carpool ChangeCarpoolDataByID(string oldCarpoolPassword, Carpool newCarpoolData)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (oldCarpoolPassword == GetCarpoolByID(newCarpoolData.CarpoolId).Password)
                {
                    string queryString =
                        $"UPDATE Users SET" +
                        $"CarpoolDriverID = @CarpoolID" +
                        $"Origin = @Origin" +
                        $"Destination = @Destination" +
                        $"FreeSpaces = @FreeSpaces" +
                        $"DepartmentTime = @DepartmentTime";
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.Add("@CarpoolID", SqlDbType.Int);
                    command.Parameters["@CarpoolID"].Value = newCarpoolData.CarpoolId;

                    command.Parameters.Add("@Origin", SqlDbType.VarChar);
                    command.Parameters["@Origin"].Value = newCarpoolData.StartingPoint;

                    command.Parameters.Add("@Destination", SqlDbType.VarChar);
                    command.Parameters["@Destination"].Value = newCarpoolData.EndingPoint;

                    command.Parameters.Add("@FreeSpaces", SqlDbType.Int);
                    command.Parameters["@FreeSpaces"].Value = newCarpoolData.FreeSpaces;

                    connection.Open();
                    command.BeginExecuteNonQuery();
                }
                else
                {
                    return null;
                }
            }
            return GetCarpoolByID(newCarpoolData.CarpoolId);
        }
    }
}
