using FlightAnalysis.Models;
using System.Globalization;
using CsvHelper;
using FlightAnalysis.Interfaces;
using FlightAnalysis.Models.DTO;

namespace FlightAnalysis.Services
{
    public class FileReader: IFileReader
    {
        private readonly ILogger<FileReader> _logger;

        public FileReader(ILogger<FileReader> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Reads the data from a csv file and returns a list of Flight object.
        /// Handles errors that can occur during the process, such as error messages.
        /// </summary>
        /// <param name="fileName">The name of the file that saved in the server</param>
        /// <returns>A List of flights objects</returns>
        /// <exception cref="Exception">Error Message</exception>
        public async Task<Result<List<Flight>>> ReadFlights(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", fileName);
            try
            {
                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    _logger.LogError($"{LanguageConstants.CSV_NOT_FOUND}: {filePath}");
                    return Result<List<Flight>>.Failure(LanguageConstants.CSV_NOT_FOUND);
                }

                using (var fileReader = new StreamReader(filePath))
                using (var csv = new CsvReader(fileReader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<FlightMap>(); // Map the csv file headers with the flight model.
                    var flights = await csv.GetRecordsAsync<Flight>().ToListAsync();
                    return Result<List<Flight>>.Success(flights);
                }
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogError($"{LanguageConstants.CSV_NOT_FOUND}: {filePath}. Exception: {ex.Message}");
                return Result<List<Flight>>.Failure(LanguageConstants.CSV_NOT_FOUND);
            }
            catch (CsvHelperException ex)
            {
                _logger.LogError($"{LanguageConstants.CSV_PROCESS_ERROR} Exception: {ex.Message}");
                return Result<List<Flight>>.Failure(LanguageConstants.CSV_PROCESS_ERROR);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{LanguageConstants.CSV_READ_UNEXPECTED_ERROR}: {ex.Message}");
                return Result<List<Flight>>.Failure(LanguageConstants.CSV_READ_UNEXPECTED_ERROR);
            }
        }
    }
}
