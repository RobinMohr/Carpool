using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Data.Interfaces
{
    public interface ICarpoolUserDataService
    {
        List<Carpool> ViewCarppolsWhereUserIsPassenger(int userID);
    }
}