
using CsvHelper.Configuration.Attributes;

namespace IB_Parser
{
    public class IB_DTO
    {
        [Index(0)] // First column
        public string ClientAccountId { get; set; }

        [Index(1)] // Second column
        public string CurrencyPrimary { get; set; }

        [Index(2)] // Third column
        public string Symbol { get; set; }

        [Index(3)]
        public string Description { get; set; }

        [Index(4)]
        public string Isin { get; set; }

        [Index(5)]
        public decimal Quantity { get; set; }

        [Index(6)]
        public decimal MarkPrice { get; set; }

         [Index(7)] 
        public decimal PositionValue { get; set; }

        [Index(8)] // This is now the 9th column from the CSV: "0.85758"
        public decimal FxRateToBase { get; set; }

        [Index(9)] // This is now the 10th column from the CSV: "NASDAQ"
        public string ListingExchange { get; set; }

        // Add a NEW property that calculates PositionValue
        public decimal CalculatedPositionValue
        {
            get
            {
                // Calculate: Position Value = Quantity * Mark Price
                return Quantity * MarkPrice;
            }
        }
    }
}