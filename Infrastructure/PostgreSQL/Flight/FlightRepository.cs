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

        public void saveFlight(Domain.Model.Flight flight)
        {
            _appDbContext.Flights.Add(flight);
            _appDbContext.SaveChanges();
        }
    }
}
