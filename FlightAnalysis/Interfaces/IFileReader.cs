using FlightAnalysis.Models;
using FlightAnalysis.Models.DTO;

namespace FlightAnalysis.Interfaces
{
    public interface IFileReader
    {
        Task<Result<List<Flight>>> ReadFlights(string fileName);
    }
}
