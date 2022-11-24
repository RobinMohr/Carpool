using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Data.Service
{
    public interface ICarpoolDataService
    {
        List<User> AllPassengersInCarpool(int carpoolID);
        Carpool ChangeCarpoolDataByID(string oldCarpoolPassword, Carpool newCarpoolData);
        List<Carpool> GetAllCarpools();
        Carpool GetCarpoolByID(int carpoolID);
    }
}