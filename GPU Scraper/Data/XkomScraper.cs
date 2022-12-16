using GPU_Scraper.Entities;
using GPUScraper.Models.Models;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System;
using GPU_Scraper.Data.Contracts;

namespace GPU_Scraper.Data
{
    public class XkomScraper : IProductScraper
    {
        private const string XKomBaseURL = "https://www.x-kom.pl/g-5/c/345-karty-graficzne.html?f1702-uklad-graficzny=24826-amd-radeon&f1702-uklad-graficzny=24827-nvidia-geforce&f1702-uklad-graficzny=262522-intel-arc";

        public IEnumerable<Product> ScrapProducts()
        {
            var web = new HtmlWeb();
            var document = web.Load(XKomBaseURL);

            //var products = document.QuerySelectorAll(".sc-3g60u5-0.dNgqYV > a.sc-1h16fat-0.dNrrmO");
            var products = document.QuerySelectorAll(".gyHdpL");
            var productsList = new List<Product>(); 

            foreach (var product in products)
            {
                var stringBuilder = new StringBuilder();
                var productName = product.QuerySelector("a > h3").Attributes["title"].Value;
                var productHref = stringBuilder.Append("https://x-kom.pl" + product.QuerySelector("a").Attributes["href"].Value).ToString();
                var productPrice = double.Parse(product.QuerySelector(".gAlJbD > span.guFePW").InnerText);

                productsList.Add(new Product 
                { 
                    Name = productName,
                    URL = productHref, 
                    Price = productPrice 
                });
            }
            
            // dodaj iteracje po stronach (paginacja na stronie bazowej)



            if (!productsList.Any())
            {
                throw new Exception();
            }

            return productsList;
        }
    }
}
