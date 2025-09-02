using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
namespace IB_Parser
{
    public class IBParser
    {
        public List<IB_DTO> ProcessFile(string filePath)
        {
            // Create a configuration for the CSV reader
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false, // NO HEADER ROW **** Important ****
                Delimiter = ",",
            };

            // Read the file
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                // This single line maps the CSV columns to the IB_DTO properties based on Index attributes
                var records = csv.GetRecords<IB_DTO>().ToList();

                // Return the collection of DTOs
                return records;
            }
        }
    }
}