using MeasurementService.Models;
using Newtonsoft.Json;

namespace MeasurementService.Logic
{
    public class JsonHandler
    {

        public Measurement convertMeasurement(string message)
        {
            Measurement measurement = JsonConvert.DeserializeObject<Measurement>(message);
            return measurement;
        }
    }
}
