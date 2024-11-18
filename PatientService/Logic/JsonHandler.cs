using Newtonsoft.Json;
using PatientService.Models;

namespace PatientService.Logic
{
    public class JsonHandler
    {
        public Patient convertPatient(string message)
        {
            Patient patient = JsonConvert.DeserializeObject<Patient>(message);
            return patient;
        }
    }
}
