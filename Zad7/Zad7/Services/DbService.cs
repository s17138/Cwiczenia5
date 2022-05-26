using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zad7.Models;
using Zad7.Models.DTO;

namespace Zad7.Services
{
    public class DbService : IDbService
    {
        private readonly mydatabaseContext _dbContext;
        public DbService(mydatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<SomeSortOfTrip>> GetTrips()
        {
            return await _dbContext.Trips
                .Select(e => new SomeSortOfTrip
                {
                    Name = e.Name,
                    Description = e.Description,
                    MaxPeople = e.MaxPeople,
                    DateFrom = e.DateFrom,
                    DateTo = e.DateTo,
                    Countries = e.CountryTrips.Select( e=> new SomeSortOfCountry { Name = e.IdCountryNavigation.Name}).ToList(),
                    Clients = e.ClientTrips.Select(e => new SomeSortOfClient { FirstName = e.IdClientNavigation.FirstName, LastName = e.IdClientNavigation.LastName})
                }).ToListAsync();
        }

       public async Task RemoveTrip(int id)
        {
            var trip = new Trip() { IdTrip = id };
            _dbContext.Attach(trip);
            _dbContext.Remove(trip);
            await _dbContext.SaveChangesAsync();
        }
    }
}
