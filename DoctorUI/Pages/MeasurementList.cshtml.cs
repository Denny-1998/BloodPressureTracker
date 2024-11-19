using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DoctorUI.Models;

namespace DoctorUI.Pages
{
    public class MeasurementListModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MeasurementListModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public string PatientSSN { get; set; }

        public List<Measurement> Measurements { get; set; }

        // This method will be triggered when the form is submitted
        public async Task OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(PatientSSN))
            {
                Measurements = new List<Measurement>(); // Clear if no Patient SSN
                return;
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"http://localhost:5000/api/Measurement/{PatientSSN}"); // Ensure this API URL is correct
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Measurements = JsonConvert.DeserializeObject<List<Measurement>>(jsonResponse);
            }
            else
            {
                Measurements = new List<Measurement>(); // Handle failure case
            }
        }
    }
}
