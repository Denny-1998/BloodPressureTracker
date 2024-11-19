using DoctorUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorUI.Pages
{
    public class MeasurementListModel : PageModel
    {
        public List<Measurement> Measurements { get; set; }

        public void OnGet()
        {
            // This should ideally come from a database or an API call
            Measurements = new List<Measurement>
        {
            new Measurement { PatientSSN = "123-45-6789", DateTime = DateTime.Now, Systolic = 120, Diastolic = 80, Seen = false },
            new Measurement { PatientSSN = "987-65-4321", DateTime = DateTime.Now, Systolic = 130, Diastolic = 85, Seen = true }
        };
        }
    }
}
