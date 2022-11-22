using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Data.Service
{
    public interface ICarpoolDataService
    {
        List<User> AllPassengersInCarpool(int carpoolID);
        List<Carpool> GetAllCarpools();
        Carpool GetCarpoolByID(int carpoolID);
        public Carpool ChangeCarpoolDataByID(Carpool newCarpoolData);

    }
}