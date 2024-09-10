namespace AirportAPI.Controllers.Flight.Post
{
    public class PostFlightBodyDTO
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string ArrivalDate { get; set; }
        public string DepartureDate { get; set; }
    }
}
