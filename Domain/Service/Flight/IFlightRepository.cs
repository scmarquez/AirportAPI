namespace AirportAPI.Domain.Service.Flight;

using AirportAPI.Domain.Model;

public interface IFlightRepository
{
    public void saveFlight(Flight flight);
}