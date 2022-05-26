using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zad7.Models.DTO;

namespace Zad7.Services
{
    public interface IDbService
    {
        Task<IEnumerable<SomeSortOfTrip>> GetTrips();
        Task RemoveTrip(int id);
    }
}
