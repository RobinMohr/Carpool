using DataService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Data.Service
{
    public class NewCarpoolDataService : INewCarpoolDataService
    {
        private readonly string connectionString = @"Data Source=localhost;Initial Catalog=CarpoolApp;Integrated Security=True;";
        readonly IUserDataService _userDataService;

        public NewCarpoolDataService(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public Carpool CreateNewCarpool(Carpool carpoolToCreate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"INSERT INTO Carpools (Password, CarpoolDriverID, Origin, Destination, FreeSpaces, DepartmentTime) " +
                                     $"OUTPUT inserted.CarpoolID " +
                                     $"VALUES (@Password, @CarpoolDriverID, @Origin, @Destination, @FreeSpaces, @DepartmentTime)";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@Password", SqlDbType.NVarChar);
                command.Parameters["@Password"].Value = carpoolToCreate.Password;

                command.Parameters.Add("@CarpoolDriverID", SqlDbType.Int);
                command.Parameters["@CarpoolDriverID"].Value = carpoolToCreate.Driver.UserID;

                command.Parameters.Add("@Origin", SqlDbType.VarChar);
                command.Parameters["@Origin"].Value = carpoolToCreate.Driver.UserID;

                command.Parameters.Add("@Destination", SqlDbType.VarChar);
                command.Parameters["@Destination"].Value = carpoolToCreate.Driver.UserID;

                command.Parameters.Add("@FreeSpaces", SqlDbType.Int);
                command.Parameters["@FreeSpaces"].Value = carpoolToCreate.Driver.UserID;

                command.Parameters.Add("@DepartmentTime", SqlDbType.SmallDateTime);
                command.Parameters["@DepartmentTime"].Value = carpoolToCreate.Time;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    carpoolToCreate.CarpoolId = (int)reader["CarpoolID"];
                    reader.Close();
                    return carpoolToCreate;
                }
                connection.Close();
                return null;
            }
        }
        public Carpool GetCarpoolByID(int carpoolID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"SELECT * FROM Carpools WHERE CarpoolID = @CarpoolID";
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@CarpoolID", SqlDbType.Int);
                command.Parameters["@CarpoolID"].Value = carpoolID;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Carpool carpool = GetCarpool(reader);
                    reader.Close();
                    return carpool;
                }
                return null;
            }
        }
        public List<Carpool> GetCarpoolsByOneParameter(string placeholder, string anotherPlaceholder)
        {
            var carpools = new List<Carpool>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"SELECT * FROM Carpools WHERE [" + placeholder.Replace("]", "]]") + "] = @test";
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@test", SqlDbType.VarChar);
                command.Parameters["@test"].Value = anotherPlaceholder;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        carpools.Add(GetCarpool(reader));
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return carpools;
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
                string querystring = "select * from carpools";
                SqlCommand command = new SqlCommand(querystring, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    allCarpools.Add(GetCarpool(reader));
                }
            }
            return allCarpools;
        }

        public List<Carpool> GetCarpoolsByMultipleParameters(List<(string, string)> ParametersToLookFor)
        {
            var carpools = new List<Carpool>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"SELECT * FROM Carpools WHERE";
                for(int i = 0; i < ParametersToLookFor.Count; i++)
                {
                    (string foo,string baa) = ParametersToLookFor[i];
                    if (i + 1 < ParametersToLookFor.Count)
                    {
                        queryString += "[" + foo.Replace("]", $"]] = @foo{i} AND");
                    }
                    else
                    {
                        queryString += "[" + foo.Replace("]", $"]] = @foo{i}");
                    }
                }

                SqlCommand command = new SqlCommand(queryString, connection);
                for (int i = 0; i < ParametersToLookFor.Count; i++)
                {
                    (string foo, string baa) = ParametersToLookFor[i];
                    command.Parameters.Add($"@foo{i}", SqlDbType.VarChar);
                    command.Parameters[$"@foo{i}"].Value = baa;
                }                    

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        carpools.Add(GetCarpool(reader));
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return carpools;
        }

    }
}
