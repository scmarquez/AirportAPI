namespace AirportAPI.Application.Flight.Query
{
    using AirportAPI.Domain.Service.Flight;
    using Domain.Model;

    public class GetFlightQueryHandler
    {
        private IFlightRepository _flightRepository;

        public GetFlightQueryHandler(IFlightRepository flightRepository) 
        {
            _flightRepository = flightRepository;
        }

        public Flight Handle(GetFlightQuery getFlightQuery)
        {
            return _flightRepository.GetFlightByID(getFlightQuery.FlightID);
        }
    }
}
