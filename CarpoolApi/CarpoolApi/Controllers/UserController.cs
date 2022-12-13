using Microsoft.AspNetCore.Mvc;
using TecAlliance.Carpools.Business.Interfaces;
using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Business.Service;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private IUserBusinessService _userBusinessService;
        private ICarpoolUserBusinessService _carpoolUserBusinessService;

        public UserController(IUserBusinessService userBusinessService, ICarpoolUserBusinessService carpoolUserBusinessService)
        {
            _userBusinessService = userBusinessService;
            _carpoolUserBusinessService = carpoolUserBusinessService;
        }

        [HttpGet]
        [Route("Users/getAll")]
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

        [HttpGet("Users/GetByID{UserID}")]
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

        [HttpPut]
        [Route("Users/ChangeUser/{OldPassword}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> UpdateUser(string OldPassword, User user)
        {
            try
            {
                return _userBusinessService.ChangeUserData(user, OldPassword);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Users/createUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> CreateNewUser(string password, string firstname, string lastname, bool canDrive)
        {
            try
            {
                return _userBusinessService.CreateNewUser(password, firstname, lastname, canDrive);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("Users/DeletedByID/{userID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> DeleteUser(int userID, string password)
        {
            try
            {
                return _userBusinessService.DeleteUserByID(userID, password);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("Users/getCarpoolsWithUserAsPassenger/{userID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CarpoolDto>>> ViewCurrentCarpools(int userID)
        {
            try
            {
                return _carpoolUserBusinessService.CurrentCarpoolsWhereUserIsPassenger(userID);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
