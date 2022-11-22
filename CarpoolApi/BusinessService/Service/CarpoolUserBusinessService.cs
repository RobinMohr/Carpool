using DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpools.Business.Interfaces;
using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Data.Interfaces;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Business.Service
{
    public class CarpoolUserBusinessService : ICarpoolUserBusinessService
    {

        private IUserBusinessService _userBusinessService;
        //private ICarpoolUserBusinessService _carpoolUserBusinessService;
        //private IUserDataService _userDataService;
        //private ICarpoolDataService _carpoolDataService;
        private ICarpoolUserDataService _carpoolUserDataService;

        public CarpoolUserBusinessService(IUserBusinessService userBusinessService, ICarpoolUserDataService carpoolUserDataService)
        {
            _userBusinessService = userBusinessService;
            _carpoolUserDataService = carpoolUserDataService;
        }

        public CarpoolDto ConvertCarpoolToDto(Carpool carpool)
        {
            if (carpool == null)
            {
                return null;
            }
            var passengersDto = new List<UserDto>();
            foreach (var passenger in carpool.Passengers)
            {
                passengersDto.Add(_userBusinessService.ConvertUserToDto(passenger));
            }
            return new CarpoolDto(carpool.CarpoolId, _userBusinessService.ConvertUserToDto(carpool.Driver), carpool.StartingPoint, carpool.EndingPoint, carpool.FreeSpaces, passengersDto, carpool.Time);
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
                var joinedCarpool =_carpoolUserDataService.JoinCarpoolAsDriver(carpoolID, userID);
                if (joinedCarpool  != null)
                {
                    return ConvertCarpoolToDto(joinedCarpool );
                }
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
    }
}
