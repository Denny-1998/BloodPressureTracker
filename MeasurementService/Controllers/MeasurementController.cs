using MeasurementService.Logic;
using Microsoft.AspNetCore.Mvc;
using MeasurementService.Models;

namespace MeasurementService.Controllers
{
    [Route("api/[controller]")]
    public class MeasurementController : Controller
    {
        private readonly MeasurementHandler _handler;

        public MeasurementController(MeasurementHandler measurementHandler) 
        {
            _handler = measurementHandler;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeasurement(int id)
        {
            Measurement measurement = await _handler.GetMeasurementById(id);


            return Ok(measurement);
        }
    }
}
