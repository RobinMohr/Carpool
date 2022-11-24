using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Business.Service
{
    public interface ICarpoolUserBusinessService
    {
        CarpoolDto ConvertCarpoolToDto(Carpool carpool);
        CarpoolDto CreateNewCarpool(CarpoolDto carpoolToCreate, string password);
        List<CarpoolDto> CurrentCarpoolsWhereUserIsPassenger(int userID);
        List<CarpoolDto> GetCarpoolsByDestination(string destination);
        List<CarpoolDto> GetCarpoolByID(int carpoolID);
        CarpoolDto JoinCarpool(int carpoolID, int userID, bool wantsToDrive);
    }
}