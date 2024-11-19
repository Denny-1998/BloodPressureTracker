using DoctorUI.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace DoctorUI.Services
{
    public class MeasurementApiService
    {
        private readonly HttpClient _httpClient;

        public MeasurementApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Measurement>> GetMeasurementsAsync(string ssn)
        {
            var response = await _httpClient.GetAsync($"https://localhost:5003/api/Patient/{ssn}/measurements");
            if (response.IsSuccessStatusCode)
            {
                var measurements = await response.Content.ReadFromJsonAsync<List<Measurement>>();
                return measurements;
            }

            return new List<Measurement>();
        }
    }

}
