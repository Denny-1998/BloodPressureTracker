using System.ComponentModel.DataAnnotations;

namespace MeasurementService.Models
{
    public class Patient
    {
        [Key]
        public string Ssn { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Measurement> Measurements { get; set; }

    }
}
