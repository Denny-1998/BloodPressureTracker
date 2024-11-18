using Microsoft.AspNetCore.Mvc;
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


        [HttpGet("{ssn}")]
        public async Task<IActionResult> Get(string ssn)
        {
            Patient patient = await _handler.GetPatientById(ssn);
            return Ok(patient);
        }
    }
}
