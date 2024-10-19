using FlightAnalysis.Models;

namespace FlightAnalysis.Test.Helper
{
    public static class FlightFactory
    {
        /// <summary>
        /// Creates a Flight object with the specified parameters.
        /// </summary>
        /// <param name="aircraftRegistrationNumber">The aircraft's registration number.</param>
        /// <param name="departureAirport">The departure airport code.</param>
        /// <param name="arrivalAirport">The arrival airport code.</param>
        /// <param name="departureDatetime">The departure date and time.</param>
        /// <param name="arrivalDatetime">The arrival date and time.</param>
        /// <returns>A new Flight object.</returns>
        public static Flight CreateFlight(string aircraftRegistrationNumber, string departureAirport, string arrivalAirport, DateTime departureDatetime, DateTime arrivalDatetime)
        {
            return new Flight
            {
                AircraftRegistrationNumber = aircraftRegistrationNumber,
                DepartureAirport = departureAirport,
                ArrivalAirport = arrivalAirport,
                DepartureDatetime = departureDatetime,
                ArrivalDatetime = arrivalDatetime
            };
        }
    }
}
