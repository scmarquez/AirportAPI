namespace AirportAPI.Domain.Service.Flight;

using AirportAPI.Domain.Model;

public interface IFlightRepository
{
    public Task saveFlight(Flight flight);
}