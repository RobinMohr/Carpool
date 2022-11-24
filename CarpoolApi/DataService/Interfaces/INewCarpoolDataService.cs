using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Data.Service
{
    public interface INewCarpoolDataService
    {
        List<User> AllPassengersInCarpool(int carpoolID);
        Carpool CreateNewCarpool(Carpool carpoolToCreate);
        Carpool GetCarpoolByID(int carpoolID);
        List<Carpool> GetCarpoolsByOneParameter(string placeholder, string anotherPlaceholder);
        List<Carpool> GetCarpoolsByMultipleParameters(List<(string, string)> ParametersToLookFor);

    }
}