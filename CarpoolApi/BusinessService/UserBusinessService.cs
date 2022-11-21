using DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpools.Business.Interfaces;
using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Data.Models;

namespace TecAlliance.Carpools.Business
{

    public class UserBusinessService : IUserBusinessService
    {
        private IUserDataService _userDataService;
        private List<User> allUser = new List<User>();

        public UserBusinessService(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }


        public List<UserDto> GetAllUser()
        {
            try
            {
                allUser = _userDataService.GetAllUser();
                List<UserDto> allUserDtos = new List<UserDto>();
                foreach (User user in allUser)
                {
                    if (!user.Deleted)
                    {
                        allUserDtos.Add(ConvertUserToDto(user));
                    }
                }
                return allUserDtos;
            }
            catch
            {
                return null;
            }
        }

        public UserDto ConvertUserToDto(User user)
        {
            return new UserDto(user.UserID, user.FirstName, user.LastName, user.CanDrive);
        }
    }
}
