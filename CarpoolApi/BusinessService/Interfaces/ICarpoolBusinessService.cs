using TecAlliance.Carpools.Business.Models;

namespace TecAlliance.Carpools.Business.Interfaces
{
    public interface ICarpoolBusinessService
    {
        List<CarpoolDto> GetAllCarpools();
        CarpoolDto GetCarpoolByID(int userID);
        CarpoolDto UpdateCarpoolByID(string carpoolPassword, CarpoolDto newCarpoolData);
    }
}