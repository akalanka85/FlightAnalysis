using CsvHelper.Configuration;
using FlightAnalysis.Models;

namespace FlightAnalysis.Services
{
    public class FlightMap: ClassMap<Flight>
    {
        public FlightMap()
        {
            Map(m => m.Id).Name("id");
            Map(m => m.AircraftRegistrationNumber).Name("aircraft_registration_number");
            Map(m => m.AircraftType).Name("aircraft_type");
            Map(m => m.FlightNumber).Name("flight_number");
            Map(m => m.DepartureAirport).Name("departure_airport");
            Map(m => m.DepartureDatetime).Name("departure_datetime");
            Map(m => m.ArrivalAirport).Name("arrival_airport");
            Map(m => m.ArrivalDatetime).Name("arrival_datetime");
        }
    }
}
