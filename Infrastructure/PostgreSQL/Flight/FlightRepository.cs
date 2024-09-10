namespace AirportAPI.Infrastructure.PostgreSQL.Flight
{
    using AirportAPI.Domain.Service.Flight;
    using Domain.Model;


    public class FlightRepository:IFlightRepository
    {
        AppDbContext _appDbContext;

        public FlightRepository(AppDbContext appDbContext) 
        { 
            _appDbContext = appDbContext;
        }

        public async Task SaveFlight(Flight flight)
        {
            await _appDbContext.Flights.AddAsync(flight);
            _appDbContext.SaveChanges();

        }

        public Flight GetFlightByID(Guid id)
        {
            return _appDbContext.Flights.Find(id);
        }

    }
}
