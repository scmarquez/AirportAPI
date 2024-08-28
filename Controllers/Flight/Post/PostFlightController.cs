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
    public StatusCodeResult Post()
    {
        Guid flighId = Guid.NewGuid();
        string sample = "asdfasdfasdf";

        RegisterFlightCommand command = new RegisterFlightCommand(flighId,sample,sample,sample,sample);

        RegisterFlightCommandHandler handler = new RegisterFlightCommandHandler(_flightRepository);

        handler.handle(command);

        return Ok();
    }
}

