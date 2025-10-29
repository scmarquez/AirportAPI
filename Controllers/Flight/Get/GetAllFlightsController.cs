using AirportAPI.Application.Flight.Query;
using AirportAPI.Domain.Service.Flight;
using Microsoft.AspNetCore.Mvc;

namespace AirportAPI.Controllers.Flight.Get;

[ApiController]
[Route("Flight")]
public class GetAllFlightsController
{
    private readonly ILogger<GetFlightController> _logger;
    private readonly IFlightRepository _flightRepository;

    public GetAllFlightsController(ILogger<GetFlightController> logger, IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
        _logger = logger;
    }

    [HttpGet]
    public List<Domain.Model.Flight> GetFlight()
    {
        var getAllFlightsQueryHandler = new GetAllFlightsQueryHandler(_flightRepository);
        return getAllFlightsQueryHandler.Handle();
    }
}