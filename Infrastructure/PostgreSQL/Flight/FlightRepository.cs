using AirportAPI.Domain.Service.Flight;

namespace AirportAPI.Infrastructure.PostgreSQL.Flight
{
    public class FlightRepository:IFlightRepository
    {
        AppDbContext _appDbContext;

        public FlightRepository(AppDbContext appDbContext) 
        { 
            _appDbContext = appDbContext;
        }

        public async Task saveFlight(Domain.Model.Flight flight)
        {
            await _appDbContext.Flights.AddAsync(flight);
            _appDbContext.SaveChanges();

        }
    }
}
