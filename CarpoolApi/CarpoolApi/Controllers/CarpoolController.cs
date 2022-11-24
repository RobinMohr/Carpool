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
        [Route("Carpols/ID/{CarpoolID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CarpoolDto>>> GetCarpoolByID(int CarpoolID)
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
        [Route("Carpols/Destination/{CarpoolDestination}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CarpoolDto>>> GetCarpoolByDestination(string CarpoolDestination)
        {
            try
            {
                return _carpoolUserBusinessService.GetCarpoolsByDestination(CarpoolDestination);
            }
            catch
            {
                return NotFound();
            }
        }


        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<List<CarpoolDto>>> GetAllCarpools()
        //{
        //    try
        //    {
        //        return _carpoolBusinessService.GetAllCarpools();
        //    }
        //    catch
        //    {
        //        return NotFound();
        //    }
        //}

        //[HttpGet("{carpoolID}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<CarpoolDto>> GetCarpoolByID(int carpoolID)
        //{
        //    try
        //    {
        //        return _carpoolBusinessService.GetCarpoolByID(carpoolID);
        //    }
        //    catch
        //    {
        //        return NotFound();
        //    }
        //}

        //[HttpPut("{carpoolID}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<CarpoolDto>> ChangeCarpoolDataByID(string carpoolPassword, CarpoolDto newCarpoolData)
        //{
        //    try
        //    {
        //        var changedCarpool = _carpoolBusinessService.UpdateCarpoolByID(carpoolPassword, newCarpoolData);
        //        if (changedCarpool == null)
        //        {
        //            return BadRequest();
        //        }
        //        return changedCarpool;
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpPut("join/{JoinID}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<CarpoolDto>> JoinCarpool(int carpoolID, int userID,bool wantsToDrive)
        //{
        //    CarpoolDto joinedCarpool = _carpoolUserBusinessService.JoinCarpool(carpoolID, userID, wantsToDrive);
        //    if (joinedCarpool == null)
        //    {
        //        return NotFound();
        //    }
        //    return joinedCarpool;
        //}
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<CarpoolDto>> CreateNewCarpool(CarpoolDto carpoolToCreate, string password)
        //{
        //    CarpoolDto addedCarpool = _carpoolUserBusinessService.CreateNewCarpool(carpoolToCreate, password);
        //    if (addedCarpool == null)
        //    {
        //        return BadRequest();
        //    }
        //    return addedCarpool;
        //}


    }
}
