namespace MeasurementService.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Systolic {  get; set; }
        public int Diastolic { get; set; }
        public bool Seen { get; set; }

        public string PatientSSN { get; set; }
        public Patient Patient { get; set; }
    }
}
