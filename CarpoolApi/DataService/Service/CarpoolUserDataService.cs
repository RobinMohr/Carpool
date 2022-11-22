using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpools.Data.Interfaces;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Data.Service
{
    public class CarpoolUserDataService : ICarpoolUserDataService
    {
        private string connectionString = @"Data Source=localhost;Initial Catalog=CarpoolApp;Integrated Security=True;";

        private ICarpoolDataService _carpoolDataService;

        public CarpoolUserDataService(ICarpoolDataService carpoolDataService)
        {
            _carpoolDataService = carpoolDataService;
        }

        public List<Carpool> ViewCarppolsWhereUserIsPassenger(int userID)
        {
            var carpools = new List<Carpool>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"SELECT * FROM CarpoolPassengers WHERE PassengerID = {userID}";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        carpools.Add(_carpoolDataService.GetCarpoolByID((int)reader["CarpoolID"]));
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return carpools;
        }

        public Carpool JoinCarpoolAsDriver()
        {

        }

        public void JoinCarpoolAsPassenger(int carpoolID, int passengerID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"INSERT INTO CarpoolPassengers" +
                $"            ([CarpoolID]" +
                $"           ,[PassengerID]" +
                $"           ,[Deleted]" +
                $"           ,[ModifiedDate])" +
                $"          VALUES" +
                $"           ({carpoolID},{passengerID},0,GETDATE())";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.BeginExecuteNonQuery();
            }
        }
    }
}
