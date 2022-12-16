namespace GPU_Scraper.Entities
{
    public class GPU
    {
        public int Id { get; set; }
        public string Vendor { get; set; }
        public string Subvendor { get; set; }
        public string Model { get; set; }
        public string ImageURL { get; set; }
        public double LowestPrice { get; set; }
        public int LowestPriceShopId { get; set; }
        public virtual Shop LowestPriceShop { get; set; }
        public double HighestPrice { get; set; }
        public int HighestPriceShopId { get; set; }
        public virtual Shop HighestPriceShop { get; set; }
    }
}
