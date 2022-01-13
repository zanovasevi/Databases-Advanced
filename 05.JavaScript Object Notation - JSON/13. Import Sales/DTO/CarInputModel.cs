using System;
using System.Collections.Generic;
using CarDealer.Models;

namespace CarDealer.DTO
{
    public class CarInputModel
    {
        public CarInputModel()
        {
            PartsId = new HashSet<int>();
        }

        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }

        public IEnumerable<int> PartsId { get; set; }

    }
}
