using Microsoft.AspNetCore.Mvc;
using TecAlliance.Carpools.Business.Interfaces;
using TecAlliance.Carpools.Business.Models;

namespace TecAlliance.Carpools.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private IUserBusinessService _userBusinessService;

        public UserController(IUserBusinessService userBusinessService)
        {
            _userBusinessService = userBusinessService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<UserDto>>> GetAllUser()
        {
            try
            {
                return _userBusinessService.GetAllUser();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{UserID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetUserByID(int UserID)
        {
            try
            {
                return _userBusinessService.GetUserByID(UserID);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
