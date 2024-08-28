namespace AirportAPI.Domain.Model;

public class Flight
{
    public Guid FlightID {  get; set; }
    public string Origin { get; set; }
    public string Destination{ get; set; }
    public string ArrivalDate{ get; set; }
    public string DepartureDate { get; set; }
}