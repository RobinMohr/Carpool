using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Data.Service
{
    public interface ICarpoolUserDataService
    {
        Carpool JoinCarpoolAsDriver(int carpoolID, int passengerID);
        void JoinCarpoolAsPassenger(int carpoolID, int passengerID);
        List<Carpool> ViewCarppolsWhereUserIsPassenger(int userID);
    }
}