namespace AirportAPI.Application.Flight.Command;

using AirportAPI.Domain.Service.Flight;
using Domain.Model;

public  class RegisterFlightCommandHandler
{
    private IFlightRepository _flightRepository;

    public RegisterFlightCommandHandler(IFlightRepository repository)
    {
        _flightRepository = repository;
    }

    public async Task handle(RegisterFlightCommand registerFlightCommand)
    {
        Flight flight = Flight.createFlight(
            registerFlightCommand.Origin,
            registerFlightCommand.Destination, 
            registerFlightCommand.ArrivalDate, 
            registerFlightCommand.DepartureDate
        );

        await _flightRepository.saveFlight(flight);
    }
}