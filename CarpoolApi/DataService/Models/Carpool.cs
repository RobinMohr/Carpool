﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpools.Data.Models
{
    public class Carpool
    {
        public int CarpoolId { get; set; }
        public User Owner { get; set; }
        public string StartingPoint { get; set; }
        public string EndingPoint { get; set; }
        public int FreeSpaces { get; set; }
        public List<User> Passengers { get; set; }
        public string Time { get; set; }
        public bool Deleted { get; set; }

        public Carpool(int carpoolId, User owner, string startingPoint, string endingPoint, int freeSpaces, List<User> passengers, string time, bool deleted)
        {
            CarpoolId = carpoolId;
            Owner = owner;
            StartingPoint = startingPoint;
            EndingPoint = endingPoint;
            FreeSpaces = freeSpaces;
            Passengers = passengers;
            Time = time;
            Deleted = deleted;
        }
    }
}
