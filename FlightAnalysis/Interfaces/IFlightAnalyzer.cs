using FlightAnalysis.Models;
using FlightAnalysis.Models.DTO;

namespace FlightAnalysis.Interfaces
{
    public interface IFlightAnalyzer
    {
        Result<List<Flight>> GetInconsistentFlights(List<Flight> flights);
    }
}
