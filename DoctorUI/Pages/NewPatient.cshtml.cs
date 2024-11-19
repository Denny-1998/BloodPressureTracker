using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DoctorUI.Models;

namespace DoctorUI.Pages
{
    public class NewPatientModel : PageModel
    {
        [BindProperty]
        public Patient NewPatient { get; set; }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Handle the new patient creation logic, like posting to RabbitMQ or an API call
                // You can use the RabbitMQ service here to post the new patient

                // Example of posting to RabbitMQ or API:
                // _rabbitMqService.PostNewPatient(NewPatient);

                // Redirect to the patient list after success
                return RedirectToPage("MeasurementList");
            }

            return Page();
        }
    }
}
