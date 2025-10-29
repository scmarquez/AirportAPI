using AirportAPI.Application.Flight.Command.DeleteFlight;
using AirportAPI.Domain.Service.Flight;
using Microsoft.AspNetCore.Mvc;

namespace AirportAPI.Controllers.Flight.Delete
{
    [Route("Flight")]
    [ApiController]
    public class DeleteFlightController : ControllerBase
    {
        private ILogger<DeleteFlightController> _logger;
        private IFlightRepository _flightRepository;

        public DeleteFlightController(ILogger<DeleteFlightController> logger, IFlightRepository flightRepository)
        {
            _logger = logger;
            _flightRepository = flightRepository;
        }

        [HttpDelete]
        public async Task<StatusCodeResult> Delete(Guid flightId)
        {
            var deleteFlightCommand = new DeleteFlightCommand(flightId);
            var deleteFlightCommandHandler = new DeleteFlifghtCommandHnadler(_flightRepository);
            
            await deleteFlightCommandHandler.Handle(deleteFlightCommand);
            return Ok();
        }
    }
}
