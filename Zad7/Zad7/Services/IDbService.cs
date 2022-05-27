using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zad7.Models.DTO;

namespace Zad7.Services
{
    public interface IDbService
    {
        Task<IEnumerable<ResponseTrip>> GetTrips();
        Task RemoveTrip(int id);
        Task<bool> RemoveClient(int id);
        Task<bool> AssignClientToTrip(int idTrip, RequestClient client);
    }
}
