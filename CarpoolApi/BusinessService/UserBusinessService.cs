using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpools.Business.Interfaces;
using TecAlliance.Carpools.Business.Models;
using TecAlliance.Carpools.Data.Interfaces;
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

        public UserDto GetUserByID(int userID)
        {
            try
            {
                return ConvertUserToDto(_userDataService.GetUserByID(userID));
            }
            catch
            {
                return null;
            }
        }

        public UserDto ChangeUserData(User user, string OldPassword)
        {
            try
            {
                return ConvertUserToDto(_userDataService.UpdateUser(user, OldPassword));
            }
            catch
            {
                return null;
            }
        }

        public UserDto CreateNewUser(string password, string firstname, string lastname, bool canDrive)
        {
            try
            {
                return ConvertUserToDto(_userDataService.AddUser(new User(0, password, firstname, lastname, canDrive, false)));

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
