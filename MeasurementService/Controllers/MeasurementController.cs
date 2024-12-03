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
            int retries = 5;
            while (retries > 0)
            {
                try
                {
                    Measurement measurement = await _handler.GetMeasurementById(id);
                    return Ok(measurement);
                }
                catch (Exception ex)
                {
                    retries--;
                    if (retries == 0)
                    {
                        Console.WriteLine(ex.Message);
                        return BadRequest("The database could not be reached. Please try again later.");
                    }
                    Console.WriteLine("Database not reached, retrying in 5 seconds");
                    Thread.Sleep(5000);
                    
                }
            }
            return BadRequest();
        }
    }
}
