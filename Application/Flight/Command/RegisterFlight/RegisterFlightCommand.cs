namespace AirportAPI.Application.Flight.Command;

public class RegisterFlightCommand
{
    public string Origin { get; }
    public string Destination{ get; }
    public string ArrivalDate{ get; }
    public string DepartureDate { get; }

    public RegisterFlightCommand (string origin, string destination, string arrivalDate, string departureDate)
    {
        Origin = origin;
        Destination = destination;
        ArrivalDate = arrivalDate;
        DepartureDate = departureDate;
    }
}