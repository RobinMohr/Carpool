using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Business.Interfaces
{
    public interface ICarpoolUserBusinessService
    {
        CarpoolDto ConvertCarpoolToDto(Carpool carpool);
        List<CarpoolDto> CurrentCarpoolsWhereUserIsPassenger(int userID);
    }
}