using AirportAPI.Domain.Service.Flight;

namespace AirportAPI.Application.Flight.Query;

public class GetAllFlightsQueryHandler
{
    private IFlightRepository _flightRepository;

    public GetAllFlightsQueryHandler(IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }

    public List<Domain.Model.Flight> Handle()
    {
        return _flightRepository.GetAllFlights();
    }
}
