using TecAlliance.Carpools.Business.Models;

namespace TecAlliance.Carpools.Business.Interfaces
{
    public interface ICarpoolBusinessService
    {
        List<CarpoolDto> GetAllCarpools();
        public CarpoolDto GetCarpoolByID(int userID);
        public CarpoolDto UpdateCarpoolByID(string ownerPassword, CarpoolDto newCarpoolData);

    }
}