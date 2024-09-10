namespace AirportAPI.Application.Flight.Query
{
    public class GetFlightQuery
    {
        public Guid FlightID { get; }

        public GetFlightQuery(Guid flightID)
        {
            FlightID = flightID;
        }

        
    }
}
