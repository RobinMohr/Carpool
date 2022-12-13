using DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpools.Business.Interfaces;
using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Data.Models;
using TecAlliance.Carpools.Data.Service;

namespace TecAlliance.Carpools.Business.Service
{
    public class CarpoolUserBusinessService : ICarpoolUserBusinessService
    {

        private IUserBusinessService _userBusinessService;
        //private ICarpoolUserBusinessService _carpoolUserBusinessService;
        //private IUserDataService _userDataService;
        //private ICarpoolDataService _carpoolDataService;
        private ICarpoolUserDataService _carpoolUserDataService;
        private INewCarpoolDataService _newCarpoolDataService;

        public CarpoolUserBusinessService(IUserBusinessService userBusinessService, ICarpoolUserDataService carpoolUserDataService, INewCarpoolDataService newCarpoolDataService)
        {
            _userBusinessService = userBusinessService;
            _carpoolUserDataService = carpoolUserDataService;
            _newCarpoolDataService = newCarpoolDataService;
        }

        public CarpoolDto ConvertCarpoolToDto(Carpool carpool)
        {
            if (carpool == null)
            {
                return null;
            }
            var passengersDto = new List<UserDto>();
            if (carpool.Passengers != null)
            {
                foreach (var passenger in carpool.Passengers)
                {
                    passengersDto.Add(_userBusinessService.ConvertUserToDto(passenger));
                }
            }
            return new CarpoolDto(carpool.CarpoolId, _userBusinessService.ConvertUserToDto(carpool.Driver), carpool.StartingPoint, carpool.EndingPoint, carpool.FreeSpaces, carpool.Time);
        }

        public List<CarpoolDto> CurrentCarpoolsWhereUserIsPassenger(int userID)
        {
            List<CarpoolDto> carpoolsWhereUserIsPassenger = new List<CarpoolDto>();
            foreach (var carpool in _carpoolUserDataService.ViewCarppolsWhereUserIsPassenger(userID))
            {
                carpoolsWhereUserIsPassenger.Add(ConvertCarpoolToDto(carpool));
            }
            return carpoolsWhereUserIsPassenger;
        }

        public CarpoolDto JoinCarpool(int carpoolID, int userID, bool wantsToDrive)
        {
            if (wantsToDrive && _userBusinessService.GetUserByID(userID).CanDrive)
            {
                var joinedCarpool = _carpoolUserDataService.JoinCarpoolAsDriver(carpoolID, userID);
                if (joinedCarpool != null)
                {
                    return ConvertCarpoolToDto(joinedCarpool);
                }
                return null;
            }

            else if (wantsToDrive && !_userBusinessService.GetUserByID(userID).CanDrive)
            {
                return null;
            }

            else
            {
                var joinedCarpool = _carpoolUserDataService.JoinCarpoolAsDriver(carpoolID, userID);
                if (joinedCarpool != null)
                {
                    return ConvertCarpoolToDto(joinedCarpool);
                }
                return null;
            }
        }
        public CarpoolDto CreateNewCarpool(CarpoolDto carpoolToCreate, string password)
        {
            return ConvertCarpoolToDto(_newCarpoolDataService.CreateNewCarpool(new Carpool(carpoolToCreate.CarpoolId, password, new User(carpoolToCreate.Driver.UserID, null, null, null, true), carpoolToCreate.StartingPoint, carpoolToCreate.EndingPoint, carpoolToCreate.FreeSpaces, null, carpoolToCreate.Time)));
        }


        public CarpoolDto GetCarpoolByID(int carpoolID)
        {            
            return ConvertCarpoolToDto(_newCarpoolDataService.GetCarpoolsByOneParameter("CarpoolID", carpoolID.ToString())[0]);
        }
        public List<CarpoolDto> GetCarpoolsByDestination(string destination)
        {
            var carpools = new List<CarpoolDto>();
            foreach (var carpool in _newCarpoolDataService.GetCarpoolsByOneParameter("Destination", destination))
            {
                carpools.Add(ConvertCarpoolToDto(carpool));
            }
            return carpools;
        }

        public List<CarpoolDto> GetCarpoolsByMultipleParameters(CarpoolDto paramsToLookFor)
        {
            List<(string,string)> values = new List<(string,string)>();

            return null;
        }



    }
}
