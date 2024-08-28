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

    public void handle(RegisterFlightCommand registerFlightCommand)
    {
        Flight flight = new Flight();

        flight.DepartureDate = registerFlightCommand.DepartureDate;
        flight.ArrivalDate = registerFlightCommand.ArrivalDate;
        flight.FlightID = registerFlightCommand.FlightId;
        flight.Destination = registerFlightCommand.Destination;
        flight.Origin = registerFlightCommand.Origin;

        _flightRepository.saveFlight(flight);
    }
}