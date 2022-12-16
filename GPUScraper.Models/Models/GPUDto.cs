using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUScraper.Models.Models
{
    public class GPUDto
    {
        public string Model { get; set; }
        public string ImageURL { get; set; }
        public double LowestPrice { get; set; }
        public string LowestPriceShop { get; set; }
        public double HighestPrice { get; set; }
        public string HighestPriceShop { get; set; }
    }
}
