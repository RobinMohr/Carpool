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
    public class CarpoolBusinessService : ICarpoolBusinessService
    {
        private ICarpoolDataService _carpoolDataService;
        private ICarpoolUserBusinessService _carpoolUserBusinessService;

        public CarpoolBusinessService(ICarpoolDataService carpoolDataService, ICarpoolUserBusinessService carpoolUserBusinessService)
        {
            _carpoolDataService = carpoolDataService;
            _carpoolUserBusinessService = carpoolUserBusinessService;
        }

        public List<CarpoolDto> GetAllCarpools()
        {
            List<CarpoolDto> allCarpoolDtos = new List<CarpoolDto>();
            foreach (var carpool in _carpoolDataService.GetAllCarpools())
            {
                allCarpoolDtos.Add(_carpoolUserBusinessService.ConvertCarpoolToDto(carpool));
            }
            return allCarpoolDtos;
        }

        public CarpoolDto GetCarpoolByID(int userID)
        {
            return _carpoolUserBusinessService.ConvertCarpoolToDto(_carpoolDataService.GetCarpoolByID(userID));
        }

        public CarpoolDto UpdateCarpoolByID(string carpoolPassword, CarpoolDto newCarpoolData)
        {
            return _carpoolUserBusinessService.ConvertCarpoolToDto(_carpoolDataService.ChangeCarpoolDataByID(
                new Carpool(
                    newCarpoolData.CarpoolId,
                    carpoolPassword,
                    new User(newCarpoolData.Driver.UserID,
                             null,
                             newCarpoolData.Driver.FirstName,
                             newCarpoolData.Driver.LastName,
                             newCarpoolData.Driver.CanDrive,
                             false
                    ),
                    newCarpoolData.StartingPoint,
                    newCarpoolData.EndingPoint,
                    newCarpoolData.FreeSpaces,
                    new List<User>(),
                    newCarpoolData.Time,
                    false)));


            //public  CarpoolDto JoinCarpool(int carpoolID, int userID, bool willDrive)
            //{
            //    if()
            //}

        }
    }
}
