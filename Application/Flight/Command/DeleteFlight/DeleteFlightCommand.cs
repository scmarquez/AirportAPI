using System;

namespace AirportAPI.Application.Flight.Command.DeleteFlight;

public class DeleteFlightCommand
{
    public Guid FlightId {get;}

    public DeleteFlightCommand(Guid flightID)
    {
        FlightId = flightID;
    }
}
