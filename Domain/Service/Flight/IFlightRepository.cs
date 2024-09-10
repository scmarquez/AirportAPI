namespace AirportAPI.Domain.Service.Flight
{
    using AirportAPI.Domain.Model;

    public interface IFlightRepository
    {
        public Task SaveFlight(Flight flight);
        public Flight GetFlightByID(Guid id);
    }
}