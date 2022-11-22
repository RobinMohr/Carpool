using Microsoft.AspNetCore.Mvc;
using TecAlliance.Carpools.Business;
using TecAlliance.Carpools.Business.Interfaces;
using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarpoolController : Controller
    {
        private ICarpoolBusinessService _carpoolBusinessService;

        public CarpoolController(ICarpoolBusinessService carpoolBusinessService)
        {
            _carpoolBusinessService = carpoolBusinessService;
        }

        [HttpGet]
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

        [HttpGet("{carpoolID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CarpoolDto>> GetCarpoolByID(int carpoolID)
        {
            try
            {
                return _carpoolBusinessService.GetCarpoolByID(carpoolID);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("{carpoolID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarpoolDto>> ChangeCarpoolDataByID(string carpoolPassword, CarpoolDto newCarpoolData)
        {
            try
            {
                return _carpoolBusinessService.UpdateCarpoolByID(carpoolPassword, newCarpoolData);
            }
            catch
            {
                return BadRequest();
            }

        }



    }
}
