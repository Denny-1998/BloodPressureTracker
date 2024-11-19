namespace DoctorUI.Models
{
    public class Measurement
    {
        public string PatientSSN { get; set; }
        public DateTime DateTime { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
        public bool Seen { get; set; }
    }

}
