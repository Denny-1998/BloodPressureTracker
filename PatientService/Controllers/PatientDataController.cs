using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using PatientService.Logic;
using PatientService.Models;

namespace PatientService.Controllers
{
    [Route("api/patient")]
    public class PatientDataController : Controller
    {
        private readonly PatientHandler _handler; 

        public PatientDataController(PatientHandler handler)
        {
            _handler = handler;
        }


        [FeatureGate("getPatient")]
        [HttpGet("{ssn}")]
        public async Task<IActionResult> Get(string ssn)
        {
            int retries = 5;
            while (retries > 0)
            {
                try
                {
                    Patient patient = await _handler.GetPatientById(ssn);
                    return Ok(patient);
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
