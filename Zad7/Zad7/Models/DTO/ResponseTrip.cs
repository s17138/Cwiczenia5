using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zad7.Models.DTO
{
    public class ResponseTrip
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int MaxPeople { get; set; }
        public IEnumerable<ResponseCountry> Countries { get; set; }
        public IEnumerable<ResponseClient> Clients { get; set; }
    }
}
