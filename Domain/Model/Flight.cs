using System.ComponentModel.DataAnnotations.Schema;

namespace AirportAPI.Domain.Model;

public sealed class Flight
{
    private Flight(
        Guid flightID,
        string origin,
        string destination,
        string arrivalDate,
        string departureDate
    ) {
        FlightID = flightID;
        Origin = origin;
        Destination = destination;
        ArrivalDate = arrivalDate;
        DepartureDate = departureDate;
    }
    [System.ComponentModel.DataAnnotations.Key]
    [Column("FlightID")]
    public Guid FlightID { get; set; }
    
    [Column("Origin")]
    public string Origin { get; set; }
    
    [Column("Destination")]
    public string Destination{ get; set; }
    
    [Column("ArrivalDate")]
    public string ArrivalDate{ get; set; }
    
    [Column("DepartureDate")]
    public string DepartureDate { get; set; }

    public static Flight createFlight(string origin, string destination, string arrivalDate, string departureDate)
    {
        return new(new Guid(), origin, destination, arrivalDate,departureDate);
    }
}