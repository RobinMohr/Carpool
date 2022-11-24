using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpools.Data.Models
{
    public class Carpool
    {
        public int CarpoolId { get; set; }
        public string Password { get; set; }
        public User Driver { get; set; }
        public string StartingPoint { get; set; }
        public string EndingPoint { get; set; }
        public int FreeSpaces { get; set; }
        public List<User> Passengers { get; set; }
        public DateTime Time { get; set; }

        public Carpool(int carpoolId,string password, User driver, string startingPoint, string endingPoint, int freeSpaces, List<User> passengers, DateTime time)
        {
            CarpoolId = carpoolId;
            Password = password;
            Driver = driver;
            StartingPoint = startingPoint;
            EndingPoint = endingPoint;
            FreeSpaces = freeSpaces;
            Passengers = passengers;
            Time = time;
        }
    }
}
