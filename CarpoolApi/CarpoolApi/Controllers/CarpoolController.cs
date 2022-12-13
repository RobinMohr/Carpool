using Microsoft.AspNetCore.Mvc;
using TecAlliance.Carpools.Business;
using TecAlliance.Carpools.Business.Interfaces;
using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Business.Service;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarpoolController : Controller
    {
        private ICarpoolBusinessService _carpoolBusinessService;
        private ICarpoolUserBusinessService _carpoolUserBusinessService;

        public CarpoolController(ICarpoolBusinessService carpoolBusinessService, ICarpoolUserBusinessService carpoolUserBusinessService)
        {
            _carpoolBusinessService = carpoolBusinessService;
            _carpoolUserBusinessService = carpoolUserBusinessService;
        }
        [HttpGet]
        [Route("Carpols/byID/{CarpoolID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarpoolDto>> GetCarpoolByID(int CarpoolID)
        {
            try
            {
                return _carpoolUserBusinessService.GetCarpoolByID(CarpoolID);               
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("Carpols/ByDestination/{CarpoolDestination}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CarpoolDto>>> GetCarpoolByDestination(string CarpoolDestination)
        {
            try
            {
                var carpoolWithGivenID = _carpoolUserBusinessService.GetCarpoolsByDestination(CarpoolDestination);
                if(carpoolWithGivenID != null)
                {
                    return carpoolWithGivenID;
                }
                return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }


        [HttpGet]
        [Route("Carpols/all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CarpoolDto>>> GetAllCarpools()
        {
            try
            {
                return _carpoolBusinessService.GetAllCarpools();
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPut]
        [Route("Carpols/Password/{carpoolPassword}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarpoolDto>> ChangeCarpoolDataByID(string carpoolPassword, CarpoolDto newCarpoolData)
        {
            try
            {
                var changedCarpool = _carpoolBusinessService.UpdateCarpoolByID(carpoolPassword, newCarpoolData);
                if (changedCarpool == null)
                {
                    return BadRequest();
                }
                return changedCarpool;
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Carpools/join/{carpoolID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarpoolDto>> JoinCarpool(int carpoolID, int userID, bool wantsToDrive)
        {
            CarpoolDto joinedCarpool = _carpoolUserBusinessService.JoinCarpool(carpoolID, userID, wantsToDrive);
            if (joinedCarpool == null)
            {
                return NotFound();
            }
            return joinedCarpool;
        }
        [HttpPost]
        [Route("Carpools/create/{carpoolID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CarpoolDto>> CreateNewCarpool(CarpoolDto carpoolToCreate, string password)
        {
            CarpoolDto addedCarpool = _carpoolUserBusinessService.CreateNewCarpool(carpoolToCreate, password);
            if (addedCarpool == null)
            {
                return BadRequest();
            }
            return addedCarpool;
        }


    }
}
