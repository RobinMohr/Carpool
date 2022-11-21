using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpools.Business.Models
{
    public class UserDto
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool CanDrive { get; set; }
        public UserDto(int userID, string firstName, string lastName, bool canDrive)
        {
            UserID = userID;
            FirstName = firstName;
            LastName = lastName;
            CanDrive = canDrive;
        }
    }
}
