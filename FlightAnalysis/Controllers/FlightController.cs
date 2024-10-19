using FlightAnalysis.Interfaces;
using FlightAnalysis.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFileReader _fileReader;
        private readonly IFlightAnalyzer _flightAnalyzer;

        public FlightController(IFileReader fileReader, IFlightAnalyzer flightAnalyzer)
        {
            _fileReader = fileReader;
            _flightAnalyzer = flightAnalyzer;
        }

        /// <summary>
        /// Read a csv file and returns the list of flights
        /// </summary>
        /// <param name="fileName">The file name that saved in the server. Default value is "flights.csv".</param>
        /// <returns>
        /// Returns an OK response with the list of flights if the operation is successful.
        /// If an error when reading the file, returns a 500 Internal Server Error with the error message.
        /// /returns>
        [HttpGet("flights/{fileName}")]
        public async Task<ActionResult<List<Flight>>> GetFlights(string fileName = "flights.csv")
        {
            // If the data set is extensive, it is better to apply the pagination.
            // Here assume that the data set is managable and the user needs all the flights in a single call. 
            var result = await _fileReader.ReadFlights(fileName);

            if (result.IsSuccess)
            {

                return Ok(result.Data);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
            }
        }

        /// <summary>
        /// Read a csv file and returns the list of inconsistent flights
        /// </summary>
        /// <param name="fileName">The file name that saved in the server. Default value is "flights.csv".</param>
        /// <returns>
        /// Returns an OK response with the list of inconsistent flights if the operation is successful.
        /// If an error when reading the file, returns a 500 Internal Server Error with the error message.
        /// /returns>
        [HttpGet("inconsistent-flights/{fileName}")]
        public async Task<ActionResult<List<Flight>>> GetInconsistentFlights(string fileName = "flights.csv")
        {
            // Read the csv file and gel all the flight informations.
            var result = await _fileReader.ReadFlights(fileName);

            if (!result.IsSuccess)
                return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);


            if (result.Data == null)
                return StatusCode(StatusCodes.Status500InternalServerError, LanguageConstants.ERROR_ANALYZING_FLIGHTS);

            var inconsistentFlightsResult = _flightAnalyzer.GetInconsistentFlights(result.Data);
            if (inconsistentFlightsResult.IsSuccess)
            {
                return Ok(inconsistentFlightsResult.Data);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, inconsistentFlightsResult.ErrorMessage);
            }

        }
    }
}
