using GPU_Scraper.Entities;
using GPUScraper.Models.Models;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System;
using System.Globalization;

namespace GPU_Scraper.Data
{
    public class XkomScraper
    {
        public IEnumerable<Product> ScrapProducts()
        {
            const string XKomBaseURL = "https://www.x-kom.pl/g-5/c/345-karty-graficzne.html?f1702-uklad-graficzny=24826-amd-radeon&f1702-uklad-graficzny=24827-nvidia-geforce&f1702-uklad-graficzny=262522-intel-arc";
            var web = new HtmlWeb();
            var document = web.Load(XKomBaseURL);
            var Urls = new List<string>() { XKomBaseURL };

            if (document.QuerySelector("a.kGuktN").Attributes["href"].Value != null)
            {
                var nextPageUrl = "https://x-kom.pl/" + document.QuerySelector("a.kGuktN").Attributes["href"].Value.ToString();
                while (nextPageUrl != null)
                {
                    Urls.Add(nextPageUrl);
                    var newDocument = web.Load(nextPageUrl);
                    if (newDocument.QuerySelector(".ieNAbN") != null)
                    {
                        break;
                    }
                    else
                    {
                        nextPageUrl = "https://x-kom.pl/" + newDocument.QuerySelector("a.kGuktN").Attributes["href"].Value.ToString();
                    }
                }
            }

            var gpusList = new List<Product>();
            NumberFormatInfo nfi = new CultureInfo("pl-PL", false).NumberFormat;

            foreach (var url in Urls)
            {
                var page = web.Load(url);
                var products = page.QuerySelectorAll(".gyHdpL");

                foreach (var product in products)
                {
                    var stringBuilder = new StringBuilder();
                    var productName = String.Join(' ', product.QuerySelector("a > h3").Attributes["title"].Value.ToString().Split(' ').Skip(2));
                    var productPrice = double.Parse(product.QuerySelector(".gAlJbD > span.guFePW").InnerText.ToString(nfi).Replace("zł", "").Replace("od", ""));

                    gpusList.Add(
                        new Product
                        {
                            Name = productName,
                            Price = productPrice,
                            Shop = "X-Kom"
                        });
                }
            }

            if (!gpusList.Any())
            {
                throw new Exception();
            }

            return gpusList;
        }
    }
}
