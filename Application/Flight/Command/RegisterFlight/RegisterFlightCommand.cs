namespace AirportAPI.Application.Flight.Command;

public class RegisterFlightCommand
{
    public Guid FlightId {  get; }
    public string Origin { get; }
    public string Destination{ get; }
    public string ArrivalDate{ get; }
    public string DepartureDate { get; }

    public RegisterFlightCommand (Guid flightId, string origin, string destination, string arrivalDate, string departureDate)
    {
        FlightId = flightId;
        Origin = origin;
        Destination = destination;
        ArrivalDate = arrivalDate;
        DepartureDate = departureDate;
    }
}