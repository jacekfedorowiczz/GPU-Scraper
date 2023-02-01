using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUScraper.Models.Models
{
    public class GPUDto
    {
        public string Name { get; set; }
        public decimal LowestPrice { get; set; }
        public string LowestPriceShop { get; set; }
        public decimal? HighestPrice { get; set; } = null;
        public string? HighestPriceShop { get; set; } = null;
    }
}
