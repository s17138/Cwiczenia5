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

        public async Task<IEnumerable<ResponseTrip>> GetTrips()
        {
            return await _dbContext.Trips
                .Select(e => new ResponseTrip
                {
                    Name = e.Name,
                    Description = e.Description,
                    MaxPeople = e.MaxPeople,
                    DateFrom = e.DateFrom,
                    DateTo = e.DateTo,
                    Countries = e.CountryTrips.Select( e=> new ResponseCountry { Name = e.IdCountryNavigation.Name}).ToList(),
                    Clients = e.ClientTrips.Select(e => new ResponseClient { FirstName = e.IdClientNavigation.FirstName, LastName = e.IdClientNavigation.LastName})
                }).ToListAsync();
        }

        public async Task<bool> RemoveClient(int id)
        {
            var clientTrips = await _dbContext.ClientTrips.Where(e => e.IdClient == id).ToListAsync();
            if(clientTrips.Count() != 0)
            {
                return false;
            }
            var client = new Client() { IdClient = id };
            _dbContext.Attach(client);
            _dbContext.Remove(client);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task RemoveTrip(int id)
        {
            var trip = new Trip() { IdTrip = id };
            _dbContext.Attach(trip);
            _dbContext.Remove(trip);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> AssignClientToTrip(int idTrip, RequestClient requestClient)
        {
            var client = await _dbContext.Clients.Where(e => e.Pesel == requestClient.Pesel).FirstOrDefaultAsync();
            if (client == null)
            {
                client = new Client()
                {
                    FirstName = requestClient.FirstName,
                    LastName = requestClient.LastName,
                    Pesel = requestClient.Pesel,
                    Email = requestClient.Email,
                    Telephone = requestClient.Telephone
                };
                _dbContext.Clients.Add(client);
                await _dbContext.SaveChangesAsync();
            }
            var clientTrip = await _dbContext.ClientTrips.Where(e => e.IdClient == client.IdClient && e.IdTrip == idTrip).FirstOrDefaultAsync();
            if (clientTrip != null)
            {
                return false;
            }
            var trip = await _dbContext.Trips.Where(e => e.IdTrip == idTrip).FirstOrDefaultAsync();
            if (trip == null)
            {
                return false;
            }
            clientTrip = new ClientTrip()
            {
                IdClient = client.IdClient,
                IdTrip = idTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = requestClient.PaymentDate
            };
            _dbContext.ClientTrips.Add(clientTrip);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
