using MeasurementService.Models;
using Microsoft.EntityFrameworkCore;

namespace MeasurementService.Logic
{
    public class MeasurementHandler
    {
        private readonly MeasurementDbContext _context;
        public MeasurementHandler(MeasurementDbContext measurementDbContext) 
        {
            _context = measurementDbContext;
        }


        public async Task StoreMeasurement(Measurement measurement)
        {
            await _context.Measurements.AddAsync(measurement);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateMeasurement(Measurement measurement)
        {

        }
        public async Task DeleteMeasurement(int id)
        {

        }
        public async Task<Measurement> GetMeasurementById(int id)
        {
            Measurement measurement = await _context.Measurements.FirstOrDefaultAsync(m => m.Id == id);
            return measurement;
        }
    }
}
