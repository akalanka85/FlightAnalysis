using FlightAnalysis.Interfaces;
using FlightAnalysis.Models;
using FlightAnalysis.Models.DTO;

namespace FlightAnalysis.Services
{
    public class FlightAnalyzer: IFlightAnalyzer
    {
        private readonly ILogger<FlightAnalyzer> _logger;

        public FlightAnalyzer(ILogger<FlightAnalyzer> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Analyze the flight data and return a list of inconsistent Flight objects.
        /// Handles errors that can occur during the process, such as error messages.
        /// </summary>
        /// <param name="flights"List of flights>A List of flights objects</param>
        /// <returns>A List of inconsistent flights objects</returns>
        /// <exception cref="Exception">Thrown when there is an error.</exception>
        public Result<List<Flight>> GetInconsistentFlights(List<Flight> flights)
        {
            try
            {
                var inconsistentFlights = new List<Flight>();

                // Use dictionary to handle large data set efficiently
                var groupedFlightByRegistrationNumber = new Dictionary<string, List<Flight>>();

               
                foreach (var flight in flights)
                {
                    if(flight.AircraftRegistrationNumber != null)
                    {
                        if (!groupedFlightByRegistrationNumber.ContainsKey(flight.AircraftRegistrationNumber))
                        {
                            groupedFlightByRegistrationNumber[flight.AircraftRegistrationNumber] = new List<Flight>();
                        }
                        groupedFlightByRegistrationNumber[flight.AircraftRegistrationNumber].Add(flight);
                    }

                }

                // Loop by each the registration number
                foreach (var flightsForRegistrationNumber in groupedFlightByRegistrationNumber.Values)
                {
                    // To Better Performance and faster access times use the .ToArray().
                    var sortedFlights = flightsForRegistrationNumber.OrderBy(f => f.DepartureDatetime).ToArray();

                    // Loop by the each flight to check the inconsistencies 
                    for (int i = 0; i < sortedFlights.Length - 1; i++)
                    {
                        var currentFlight = sortedFlights[i];
                        var nextFlight = sortedFlights[i + 1];

                        // Check arrival airport of current flight is mtach with the departure of next flight
                        if (currentFlight.ArrivalAirport != nextFlight.DepartureAirport)
                        {
                            inconsistentFlights.Add(currentFlight);
                            inconsistentFlights.Add(nextFlight);
                        }
                    }
                }

                return Result<List<Flight>>.Success(inconsistentFlights);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{LanguageConstants.ERROR_ANALYZING_FLIGHTS}. Exception: {ex.Message}");
                return Result<List<Flight>>.Failure(LanguageConstants.ERROR_ANALYZING_FLIGHTS);

            }
        }
    }
}
