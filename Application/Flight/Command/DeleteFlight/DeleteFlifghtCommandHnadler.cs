using System;
using AirportAPI.Domain.Service.Flight;

namespace AirportAPI.Application.Flight.Command.DeleteFlight;

public class DeleteFlifghtCommandHnadler
{
    private IFlightRepository repository;

    public DeleteFlifghtCommandHnadler(IFlightRepository repository)
    {
        this.repository = repository;
    }

    public async Task Handle(DeleteFlightCommand command)
    {
        await repository.DeleteFlight(command.FlightId);
    }
}
