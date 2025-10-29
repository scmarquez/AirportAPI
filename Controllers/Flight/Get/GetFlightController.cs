namespace AirportAPI.Controllers.Flight.Get
{
    using AirportAPI.Application.Flight.Query;
    using AirportAPI.Domain.Model;
    using AirportAPI.Domain.Service.Flight;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("Flight")]
    public class GetFlightController
    {
        private readonly ILogger<GetFlightController> _logger;
        private readonly IFlightRepository _flightRepository;

        public GetFlightController(ILogger<GetFlightController> logger, IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("{flightID}")]
        public Flight GetFlight(Guid flightID)
        {
            var getFlightQuery = new GetFlightQuery(flightID);
            var getFlightQueryHandler = new GetFlightQueryHandler(_flightRepository);
            try
            {
                return getFlightQueryHandler.Handle(getFlightQuery);
            }
            catch (Exception e)
            {
                throw new Exception($"Error getting flight with ID {flightID}: {e.Message}");
            }
        }
    }
}
