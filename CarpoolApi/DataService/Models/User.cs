using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpools.Data.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool CanDrive { get; set; }
        public User(int userID, string password, string firstName, string lastName, bool canDrive)
        {
            UserID = userID;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            CanDrive = canDrive;
        }
    }
}
