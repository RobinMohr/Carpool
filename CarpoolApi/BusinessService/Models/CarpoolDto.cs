using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpools.Business.Models
{
    public class CarpoolDto
    {
        public int CarpoolId { get; set; }
        public UserDto Driver { get; set; }
        public string StartingPoint { get; set; }
        public string EndingPoint { get; set; }
        public int FreeSpaces { get; set; }
        public List<UserDto> Passengers { get; set; }
        public string Time { get; set; }

        public CarpoolDto(int carpoolId, UserDto driver, string startingPoint, string endingPoint, int freeSpaces, List<UserDto> passengers, string time)
        {
            CarpoolId = carpoolId;
            Driver = driver;
            StartingPoint = startingPoint;
            EndingPoint = endingPoint;
            FreeSpaces = freeSpaces;
            Passengers = passengers;
            Time = time;
        }
    }
}
