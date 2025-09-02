using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;


namespace IB_Parser
{
    public class Program
    {
        static void Main(string[] args)
        {

            TestParser();

            // Keep the console window open until a key is pressed
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();

        }

        static void TestParser()
        {
            // Create an instance of parser
            var parser = new IBParser();

            
            // "Positions.csv" in the project directory
            string filePath = "Positions.csv";

            // Check if the file exists before reading
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File not found at {Path.GetFullPath(filePath)}");
                return;
            }

            try
            {
                // Process the file 
                Console.WriteLine($"Reading file: {filePath}");
                List<IB_DTO> records = parser.ProcessFile(filePath);

                // Display the results
                Console.WriteLine($"Successfully read {records.Count} records:\n");

                // Display each record
                for (int i = 0; i < records.Count; i++)
                {
                    var record = records[i];
                    Console.WriteLine($"--- Record {i + 1} ---");
                    Console.WriteLine($"Account: {record.ClientAccountId}");
                    Console.WriteLine($"Symbol: {record.Symbol} ({record.Description})");
                    Console.WriteLine($"ISIN: {record.Isin}");
                    Console.WriteLine($"Quantity: {record.Quantity}");
                    Console.WriteLine($"Price: {record.MarkPrice}");

                    // Use the calculated value instead of the parsed value
                    Console.WriteLine($"Value: {record.CalculatedPositionValue} {record.CurrencyPrimary}");

                    // Show the FX Rate that was parsed from the CSV (now at index 8)
                    Console.WriteLine($"FX Rate: {record.FxRateToBase}");

                    Console.WriteLine($"Exchange: {record.ListingExchange}");
                    Console.WriteLine(); // Empty line for readability
                }
                decimal totalPortfolioValue = records.Sum(record => record.CalculatedPositionValue);
                Console.WriteLine("=========================================");
                Console.WriteLine($"TOTAL PORTFOLIO VALUE: {totalPortfolioValue} {records[0].CurrencyPrimary}");
                Console.WriteLine(" ");

                decimal totalCsvPositionValue = records.Sum(record => record.PositionValue);
                Console.WriteLine($"(Sum of CSV Position Values: {totalCsvPositionValue} {records[0].CurrencyPrimary})");

            }
            catch (Exception ex)
            {
                // Handle errors 
                Console.WriteLine($"An error occurred: {ex.Message}");
                
            }
        }




    }
}
