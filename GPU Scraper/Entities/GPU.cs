namespace GPU_Scraper.Entities
{
    public class GPU
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal LowestPrice { get; set; }
        public string LowestPriceShop { get; set; }
        public decimal? HighestPrice { get; set; }
        public string? HighestPriceShop { get; set; }
    }
}
