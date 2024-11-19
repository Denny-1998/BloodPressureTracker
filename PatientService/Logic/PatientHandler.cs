using Microsoft.EntityFrameworkCore;
using PatientService.Models;

namespace PatientService.Logic
{
    public class PatientHandler
    {
        private readonly PatientDbContext _context;
        public PatientHandler(PatientDbContext patientDbContext)
        {
            _context = patientDbContext;
        }


        public async Task StorePatient(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }
        
        public async Task<Patient> GetPatientById(string Ssn)
        {
            Patient patient = await _context.Patients.FirstOrDefaultAsync(p => p.Ssn == Ssn);
            return patient;
        }
    }
}
