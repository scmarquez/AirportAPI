using AirportAPI.Application.Flight.Command;
using AirportAPI.Domain.Service.Flight;
using Microsoft.AspNetCore.Mvc;

namespace AirportAPI.Controllers.Flight.Post;

[ApiController]
[Route("Flight")]
public class PostFlightController : ControllerBase
{
    private readonly ILogger<PostFlightController> _logger;
    private readonly IFlightRepository _flightRepository;

    public PostFlightController(ILogger<PostFlightController> logger, IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
        _logger = logger;
    }

    [HttpPost]
    public async Task<StatusCodeResult> Post(PostFlightBodyDTO postFlightBodyDTO)
    {
        RegisterFlightCommand command = new RegisterFlightCommand(
            postFlightBodyDTO.Origin,
            postFlightBodyDTO.Destination,
            postFlightBodyDTO.ArrivalDate, 
            postFlightBodyDTO.DepartureDate 
        );

        RegisterFlightCommandHandler handler = new RegisterFlightCommandHandler(_flightRepository);

        await handler.handle(command);

        return Ok();
    }
}

