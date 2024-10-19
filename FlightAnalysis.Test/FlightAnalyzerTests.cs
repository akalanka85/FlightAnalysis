using FlightAnalysis.Models;
using FlightAnalysis.Services;
using FlightAnalysis.Test.Helper;
using Microsoft.Extensions.Logging;
using Moq;

namespace FlightAnalysis.Test
{
    public class FlightAnalyzerTests
    {
        private readonly Mock<ILogger<FlightAnalyzer>> _mockLogger;
        private readonly FlightAnalyzer _flightAnalyzer;

        public FlightAnalyzerTests()
        {
            _mockLogger = new Mock<ILogger<FlightAnalyzer>>();
            _flightAnalyzer = new FlightAnalyzer(_mockLogger.Object);
        }


        /// <summary>
        /// Test if there are inconsistencies, it should return the inconsistent flight list.
        /// </summary>
        [Fact]
        public void GetInconsistentFlights_IfInconsistencies_ReturnsInconsistentFlights()
        {
            var flights = new List<Flight>
            {
                FlightFactory.CreateFlight("ZX-IKD", "HEL", "DXB", new DateTime(2024, 10, 18, 6, 0, 0), new DateTime(2024, 10, 18, 10, 0, 0)),
                FlightFactory.CreateFlight("ZX-IKD", "SFO", "CDG", new DateTime(2024, 10, 19, 8, 0, 0), new DateTime(2024, 10, 19, 12, 0, 0))
            };

            var result = _flightAnalyzer.GetInconsistentFlights(flights);

            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Data);
            Assert.Equal(2, result.Data.Count);
            Assert.Equal("DXB", result.Data[0].ArrivalAirport);
            Assert.Equal("SFO", result.Data[1].DepartureAirport);
        }


        /// <summary>
        ///  Test if there are no inconsistencies, it should return the rmpty list.
        /// </summary>
        [Fact]
        public void GetInconsistentFlights_IfNoInconsistencies_ReturnsEmptyFlightList()
        {

            var flights = new List<Flight>
            {
                FlightFactory.CreateFlight("ZX-IKD", "HEL", "DXB", new DateTime(2024, 10, 18, 6, 0, 0), new DateTime(2024, 10, 18, 10, 0, 0)),
                FlightFactory.CreateFlight("ZX-IKD", "DXB", "HEL", new DateTime(2024, 10, 19, 8, 0, 0), new DateTime(2024, 10, 19, 12, 0, 0))
            };

            var result = _flightAnalyzer.GetInconsistentFlights(flights);

            Assert.True(result.IsSuccess);
            Assert.Empty(result.Data);
        }

        /// <summary>
        /// If there is error, it should return the correct error message
        /// </summary>
        [Fact]
        public void GetInconsistentFlights_IfThrowsException_ReturnsFailureResult()
        {
            List<Flight> flights = null;

            var result = _flightAnalyzer.GetInconsistentFlights(flights);

            Assert.False(result.IsSuccess);
            Assert.Equal(LanguageConstants.ERROR_ANALYZING_FLIGHTS, result.ErrorMessage);
        }
    }
}
