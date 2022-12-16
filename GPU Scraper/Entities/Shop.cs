namespace GPU_Scraper.Entities
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public List<GPU> GPUs { get; set; }
    }
}
