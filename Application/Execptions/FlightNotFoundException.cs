namespace AirportAPI.Application.Execptions;

public class FlightNotFoundException: Exception
{
    public FlightNotFoundException(Guid flightId) : base("Flight with id " + flightId + " not found") { }
}