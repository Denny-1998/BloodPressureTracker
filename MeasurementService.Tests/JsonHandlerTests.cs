using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeasurementService.Logic;

namespace MeasurementService.Tests
{
    public class JsonHandlerTests
    {
        private readonly JsonHandler _jsonHandler;

        public JsonHandlerTests()
        {
            _jsonHandler = new JsonHandler();
        }

        [Fact]
        public void ConvertMeasurement_ValidJson_ReturnsMeasurement()
        {
            // Arrange
            var json = @"{
                'Id': 1,
                'DateTime': '2024-01-01T12:00:00',
                'Systolic': 120,
                'Diastolic': 80,
                'Seen': false,
                'PatientSSN': '12345678'
            }";

            // Act
            var result = _jsonHandler.convertMeasurement(json);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(120, result.Systolic);
            Assert.Equal(80, result.Diastolic);
            Assert.Equal("12345678", result.PatientSSN);
        }

    }
}
